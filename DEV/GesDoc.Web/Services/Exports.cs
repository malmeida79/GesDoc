using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Reflection;
using System.Text;
using System.Web;
using ClosedXML.Excel;
using System.Web.UI.WebControls;

namespace GesDoc.Web.Services
{
    public static class Exports
    {
        /// <summary>
        /// Exportando lista generica para CSV
        /// </summary>
        /// <typeparam name="T">Tipo da lista a ser Exportada</typeparam>
        /// <param name="lista">Lista a ser Exportada</param>
        /// <param name="fileName">Nome do arquivo de saida</param>
        public static void ListToCSV<T>(this List<T> lista, string fileName)
        {
            StringBuilder sb = new StringBuilder();

            //Get the properties for type T for the headers
            PropertyInfo[] propInfos = typeof(T).GetProperties();
            for (int i = 0; i <= propInfos.Length - 1; i++)
            {
                sb.Append(propInfos[i].Name);

                if (i < propInfos.Length - 1)
                {
                    sb.Append(";");
                }
            }

            sb.AppendLine();

            //Loop through the collection, then the properties and add the values
            for (int i = 0; i <= lista.Count - 1; i++)
            {
                T item = lista[i];
                for (int j = 0; j <= propInfos.Length - 1; j++)
                {
                    object o = item.GetType().GetProperty(propInfos[j].Name).GetValue(item, null);
                    if (o != null)
                    {
                        string value = o.ToString();

                        //Check if the value contans a comma and place it in quotes if so
                        if (value.Contains(";"))
                        {
                            value = string.Concat("\"", value, "\"");
                        }

                        //Replace any \r or \n special characters from a new line with a space
                        if (value.Contains("\r"))
                        {
                            value = value.Replace("\r", " ");
                        }
                        if (value.Contains("\n"))
                        {
                            value = value.Replace("\n", " ");
                        }

                        sb.Append(value);
                    }

                    if (j < propInfos.Length - 1)
                    {
                        sb.Append(";");
                    }
                }

                sb.AppendLine();
            }

            HttpContext.Current.Response.Clear();
            HttpContext.Current.Response.AddHeader("content-disposition", $"attachment; filename={fileName}.csv");
            HttpContext.Current.Response.ContentType = "text/csv";
            HttpContext.Current.Response.AddHeader("Pragma", "public");
            HttpContext.Current.Response.Write(sb.ToString());
            HttpContext.Current.Response.End();
        }

        /// <summary>
        /// Exporta lista generica para excel
        /// </summary>
        /// <typeparam name="T">Tipo da lista a ser Exportada</typeparam>
        /// <param name="lista">Lista a ser Exportada</param>
        /// <param name="fileName">Nome do arquivo de saida</param>
        public static void ListToExcel<T>(this List<T> lista, string fileName)
        {
            //1. convert the list to datatable 
            DataTable dt = lista.GetDataTableFromList<T>();

            //2. using the following code to export datatable to excel.
            using (XLWorkbook wb = new XLWorkbook())
            {
                wb.Worksheets.Add(dt, fileName);

                HttpContext.Current.Response.Clear();
                HttpContext.Current.Response.Buffer = true;
                HttpContext.Current.Response.Charset = "";
                HttpContext.Current.Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                HttpContext.Current.Response.AddHeader("content-disposition", $"attachment;filename={fileName}.xlsx");
                using (MemoryStream MyMemoryStream = new MemoryStream())
                {
                    wb.SaveAs(MyMemoryStream);
                    MyMemoryStream.WriteTo(HttpContext.Current.Response.OutputStream);
                    HttpContext.Current.Response.Flush();
                    HttpContext.Current.Response.End();
                }
            }
        }

        /// <summary>
        /// Exportando lista generica para CSV
        /// </summary>
        /// <typeparam name="T">Tipo da lista a ser Exportada</typeparam>
        /// <param name="lista">Lista a ser Exportada</param>
        /// <param name="fileName">Nome do arquivo de saida</param>
        public static void ListToTXT<T>(this List<T> lista, string fileName)
        {
            StringBuilder sb = new StringBuilder();

            //Get the properties for type T for the headers
            PropertyInfo[] propInfos = typeof(T).GetProperties();
            for (int i = 0; i <= propInfos.Length - 1; i++)
            {
                sb.Append(propInfos[i].Name);

                if (i < propInfos.Length - 1)
                {
                    sb.Append(",");
                }
            }

            sb.AppendLine();

            //Loop through the collection, then the properties and add the values
            for (int i = 0; i <= lista.Count - 1; i++)
            {
                T item = lista[i];
                for (int j = 0; j <= propInfos.Length - 1; j++)
                {
                    object o = item.GetType().GetProperty(propInfos[j].Name).GetValue(item, null);
                    if (o != null)
                    {
                        string value = o.ToString();

                        //Check if the value contans a comma and place it in quotes if so
                        if (value.Contains(","))
                        {
                            value = string.Concat("\"", value, "\"");
                        }

                        //Replace any \r or \n special characters from a new line with a space
                        if (value.Contains("\r"))
                        {
                            value = value.Replace("\r", " ");
                        }
                        if (value.Contains("\n"))
                        {
                            value = value.Replace("\n", " ");
                        }

                        sb.Append(value);
                    }

                    if (j < propInfos.Length - 1)
                    {
                        sb.Append(",");
                    }
                }

                sb.AppendLine();
            }

            HttpContext.Current.Response.Clear();
            HttpContext.Current.Response.AddHeader("content-disposition", $"attachment; filename={fileName}.txt");
            HttpContext.Current.Response.ContentType = "text/plain";
            HttpContext.Current.Response.AddHeader("Pragma", "public");
            HttpContext.Current.Response.Write(sb.ToString());
            HttpContext.Current.Response.End();
        }

        /// <summary>
        /// Exportando grid view para CSV
        /// </summary>
        /// <param name="dgv"></param>
        /// <param name="fileName"></param>
        public static void ExportGridToCSV(this GridView dgv, string fileName)
        {
            // Don't save if no data is returned
            if (dgv.Rows.Count == 0)
            {
                return;
            }

            StringBuilder sb = new StringBuilder();

            // Column headers
            string columnsHeader = "";

            for (int i = 0; i < dgv.Columns.Count; i++)
            {
                columnsHeader += dgv.Columns[i].HeaderText + ";";
            }

            sb.Append(columnsHeader + Environment.NewLine);

            // Go through each cell in the datagridview
            foreach (GridViewRow dgvRow in dgv.Rows)
            {
                for (int c = 1; c < dgvRow.Cells.Count; c++)
                {
                    // Append the cells data followed by a comma to delimit.
                    sb.Append(dgvRow.Cells[c].Text + ";");
                }
                // Add a new line in the text file.
                sb.Append(Environment.NewLine);
            }

            HttpContext.Current.Response.Clear();
            HttpContext.Current.Response.AddHeader("content-disposition", $"attachment; filename={fileName}.csv");
            HttpContext.Current.Response.ContentType = "text/csv";
            HttpContext.Current.Response.Charset = "iso-88859-1";
            HttpContext.Current.Response.AddHeader("Pragma", "public");
            HttpContext.Current.Response.Write(sb.ToString());
            HttpContext.Current.Response.End();
        }

        /// <summary>
        /// Export Grid View to TXT
        /// </summary>
        /// <param name="dgv"></param>
        /// <param name="fileName"></param>
        public static void ExportGridToTXT(this GridView dgv, string fileName)
        {

            // Don't save if no data is returned
            if (dgv.Rows.Count == 0)
            {
                return;
            }

            StringBuilder sb = new StringBuilder();

            // Column headers
            string columnsHeader = "";

            for (int i = 0; i < dgv.Columns.Count; i++)
            {
                columnsHeader += dgv.Columns[i].HeaderText + ",";
            }

            sb.Append(columnsHeader + Environment.NewLine);

            // Go through each cell in the datagridview
            foreach (GridViewRow dgvRow in dgv.Rows)
            {
                for (int c = 1; c < dgvRow.Cells.Count; c++)
                {
                    // Append the cells data followed by a comma to delimit.
                    sb.Append(dgvRow.Cells[c].Text + ",");
                }
                // Add a new line in the text file.
                sb.Append(Environment.NewLine);
            }

            HttpContext.Current.Response.Clear();
            HttpContext.Current.Response.AddHeader("content-disposition", $"attachment; filename={fileName}.csv");
            HttpContext.Current.Response.ContentType = "text/csv";
            HttpContext.Current.Response.Charset = "iso-88859-1";
            HttpContext.Current.Response.AddHeader("Pragma", "public");
            HttpContext.Current.Response.Write(sb.ToString());
            HttpContext.Current.Response.End();
        }
    }
}