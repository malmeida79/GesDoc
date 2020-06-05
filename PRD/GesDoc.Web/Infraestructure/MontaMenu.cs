using System.Text;

namespace GesDoc.Web.Infraestructure
{
    public static class MontaMenu
    {
        /// <summary>
        /// Fecha item pai que foi adicionado
        /// </summary>
        /// <returns></returns>
        public static string FechaItemPai()
        {
            StringBuilder itemPai = new StringBuilder();
            itemPai.AppendLine(@"</ul>");
            itemPai.AppendLine(@"</li>");
            return itemPai.ToString();
        }

        public static string AdicionaPai(string item, int seq)
        {
            StringBuilder itemPai = new StringBuilder();

            itemPai.AppendLine(@"<li class=""dropdown"">");
            itemPai.AppendLine($@"<a class=""dropdown-toggle"" href=""#"" id=""drop{seq.ToString()}"" data-toggle=""dropdown"">");
            itemPai.AppendLine(item);
            itemPai.AppendLine(@"</a>");
            itemPai.AppendLine(@"<ul class=""dropdown-menu"">");

            return itemPai.ToString();
        }

        /// <summary>
        /// Criar item em uma barra
        /// </summary>
        /// <param name="item">descrição item</param>
        /// <param name="link">link do item</param>
        /// <returns></returns>
        public static string AdicionaItem(string item, string link)
        {
            return $@"<li><a href=""{link}"">{item}</a></li>";
        }
    }
}