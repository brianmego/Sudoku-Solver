using HtmlAgilityPack;
using System.Collections.Generic;

namespace Sudoku_Solver.Models
{
    class WebHarvester
    {
        public WebHarvester()
        {
            HtmlWeb htmlWeb = new HtmlWeb();
            HtmlDocument document = htmlWeb.Load("http://show.websudoku.com/?level=1");
            HtmlNodeCollection allInputs = document.DocumentNode.SelectNodes("//input");
            List<HtmlNode> allSudokuSlots = new List<HtmlNode>();
            foreach (HtmlNode node in allInputs)
            {
                if (node.Id.Length>0 && node.Id[0] == 'f')
                {
                    allSudokuSlots.Add(node);
                }
            }
        }
        
    }
}
