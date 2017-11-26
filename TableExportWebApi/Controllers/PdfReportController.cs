using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using iTextSharp.text;
using iTextSharp.text.pdf;

using TableExportWebApi.Models;
using System.Net.Http;
using System.Net;
using System.IO;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace TableExportWebApi.Controllers
{
    [Route("api/pdf-report")]
    public class PdfReportController : Controller
    {
        CustomerDataContext customerDataContext;
        FieldNameContext fieldNameContext;

        private PdfPTable getTable()
        {
            PdfPTable table = new PdfPTable(fieldNameContext.FieldNames.Count());

            HashSet<long> customerIds = new HashSet<long>();
            foreach (var customer in customerDataContext.CustomerDatas)
            {
                long id = customer.customerId;
                customerIds.Add(id);
            }

            // Table header
            foreach (var fieldName in fieldNameContext.FieldNames)
            {
                table.AddCell(fieldName.name);
            }

            // Table contents
            foreach (var customerId in customerIds)
            {
                foreach (var fieldName in fieldNameContext.FieldNames)
                {
                    var selectedCustomerData = customerDataContext.CustomerDatas.FirstOrDefault(
                        c => (c.customerId == customerId && c.fieldName == fieldName.name));
                    if (selectedCustomerData!=null && selectedCustomerData.fieldValue!=null)
                    {
                        table.AddCell(selectedCustomerData.fieldValue);
                    } 
                    else
                    {
                        table.AddCell("");
                    }
                }
            }

            return table;
        }

        public PdfReportController(CustomerDataContext c, FieldNameContext f)
        {
            customerDataContext = c;
            fieldNameContext = f;
        }

        [HttpGet]
        public IActionResult Get()
        {
            // New PDF document
            MemoryStream workStream = new MemoryStream();
            Document document = new Document();
            PdfWriter.GetInstance(document, workStream).CloseStream = false;


            document.Open();

            document.Add(new Paragraph("Danh sách khách hàng"));
            document.Add(getTable());

            document.Close();
            byte[] byteInfo = workStream.ToArray();
            workStream.Write(byteInfo, 0, byteInfo.Length);
            workStream.Position = 0;

            return new FileStreamResult(workStream, "application/pdf");
        }

    }
}
