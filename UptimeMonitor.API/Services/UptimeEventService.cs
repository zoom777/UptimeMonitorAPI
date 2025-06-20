using ClosedXML.Excel;
using Microsoft.EntityFrameworkCore;
using UptimeMonitor.API.Data;
using UptimeMonitor.API.DTOs;
using UptimeMonitor.API.Interfaces;

namespace UptimeMonitor.API.Services
{
    public class UptimeEventService : IUptimeEventService
    {
        private readonly AppDbContext _context;

        public UptimeEventService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<UptimeEventResponseDto>> GetAllAsync()
        {
            return await _context.UptimeEvents
                .Include(e => e.UptimeCheck)
                    .ThenInclude(uc => uc.Component)
                    .ThenInclude(c => c.ServiceSystem)
                    .OrderByDescending(e => e.StartTime)
                .Select(e => new UptimeEventResponseDto
                {
                    Id = e.Id,
                    UptimeCheckId = e.UptimeCheckId,
                    ComponentId = e.UptimeCheck.ComponentId,
                    ServiceSystemId = e.UptimeCheck.Component.ServiceSystemId,
                    StartTime = e.StartTime,
                    EndTime = e.EndTime,
                    IsUp = e.IsUp,
                    IsFalsePositive = e.IsFalsePositive,
                    Category = e.Category,
                    Note = e.Note,
                    JiraTicket = e.JiraTicket,
                    MaintenanceType = e.MaintenanceType
                }).ToListAsync();
        }

        public async Task<IEnumerable<UptimeEventExportDto>> GetAllForExportAsync()
        {
            return await _context.UptimeEvents
                .Include(e => e.UptimeCheck)
                    .ThenInclude(uc => uc.Component)
                    .ThenInclude(c => c.ServiceSystem)
                    .OrderByDescending(e => e.StartTime)
                .Select(e => new UptimeEventExportDto
                {
                    Id = e.Id,
                    UptimeCheckName = e.UptimeCheck.Name,
                    ComponentName = e.UptimeCheck.Component.Name,
                    ServiceSystemName = e.UptimeCheck.Component.ServiceSystem.Name,
                    StartTime = e.StartTime,
                    EndTime = e.EndTime,
                    IsUp = e.IsUp,
                    IsFalsePositive = e.IsFalsePositive,
                    Category = e.Category,
                    Note = e.Note,
                    JiraTicket = e.JiraTicket,
                    MaintenanceType = e.MaintenanceType
                }).ToListAsync();
        }

        public async Task<byte[]> GenerateExcelAsync()
        {
            var data = await GetAllForExportAsync();

            using var workbook = new XLWorkbook();
            var worksheet = workbook.Worksheets.Add("Uptime Events");

            var headers = new[]
            {
                "ID", "Check Name", "Component", "System",
                "Start Time", "End Time", "Is Up", "False Positive",
                "Category", "Note", "Jira", "Maintenance Type"
            };

            for (int i = 0; i < headers.Length; i++)
            {
                worksheet.Cell(1, i + 1).Value = headers[i];
                worksheet.Cell(1, i + 1).Style.Font.Bold = true;
            }

            // Data
            int row = 2;
            foreach (var item in data)
            {
                worksheet.Cell(row, 1).Value = item.Id;
                worksheet.Cell(row, 2).Value = item.UptimeCheckName;
                worksheet.Cell(row, 3).Value = item.ComponentName;
                worksheet.Cell(row, 4).Value = item.ServiceSystemName;
                worksheet.Cell(row, 5).Value = item.StartTime;
                worksheet.Cell(row, 6).Value = item.EndTime;
                worksheet.Cell(row, 7).Value = item.IsUp;
                worksheet.Cell(row, 8).Value = item.IsFalsePositive;
                worksheet.Cell(row, 9).Value = item.Category;
                worksheet.Cell(row, 10).Value = item.Note;
                worksheet.Cell(row, 11).Value = item.JiraTicket;
                worksheet.Cell(row, 12).Value = item.MaintenanceType;
                row++;
            }

            // Aplicar autofit
            worksheet.Columns().AdjustToContents();

            using var stream = new MemoryStream();
            workbook.SaveAs(stream);
            return stream.ToArray();
        }

        public async Task<UptimeEventUpdateResponseDto?> UpdatePartialAsync(int id, UptimeEventUpdateDto dto)
        {
            var entity = await _context.UptimeEvents.FindAsync(id);
            if (entity == null)
                return null;

            entity.IsFalsePositive = dto.IsFalsePositive;
            entity.Category = dto.Category;
            entity.Note = dto.Note;
            entity.JiraTicket = dto.JiraTicket;
            entity.MaintenanceType = dto.MaintenanceType;

            await _context.SaveChangesAsync();

            return new UptimeEventUpdateResponseDto
            {
                Id = entity.Id,
                IsFalsePositive = entity.IsFalsePositive,
                Category = entity.Category,
                Note = entity.Note,
                JiraTicket = entity.JiraTicket,
                MaintenanceType = entity.MaintenanceType
            };
        }
    }
}