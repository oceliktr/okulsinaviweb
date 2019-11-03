using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;
using DAL;

/// <summary>
/// Summary description for ListboxWithTooltip
/// </summary>
public class ListboxWithTooltip
{
    public void Kazanimlar(ListBox lb, int sinif, int brans, int ogrenmeAlani, int altOgrenmeAlani)
    {
        KazanimlarDB kazanimDb = new KazanimlarDB();
        lb.DataSource = kazanimDb.KayitlariGetir(brans, sinif, ogrenmeAlani, altOgrenmeAlani);
        lb.DataValueField = "Id";
        lb.DataTextField = "Kazanim";
        lb.DataBind();

        //Mouse üzerine getirilince tooltip gösterilmesi için.
        foreach (ListItem item in lb.Items)
        {
            item.Attributes.Add("title", item.Text);
        }
    }
}