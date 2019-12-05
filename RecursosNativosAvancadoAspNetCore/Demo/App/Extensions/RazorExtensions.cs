using System;
using Microsoft.AspNetCore.Mvc.Razor;

namespace App.Extensions
{
    public static class RazorExtensions
    {
        public static string StatusPedido(this RazorPage page, bool status)
        {
            switch (status)
            {
                case true:
                    return "<span class='badge badge-success'>Ativo</span>";
                case false:
                    return "<span class='badge badge-danger'>Inativo</span>";
            }

            return "";
        }
    }
}