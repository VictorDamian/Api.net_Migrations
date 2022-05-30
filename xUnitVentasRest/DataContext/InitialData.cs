using ApiVentas.Repositories;
using ApiVentas.Models;
using ApiVentas.Models.DTOs;
using AutoMapper;
using System.Collections.Generic;

namespace xUnitVentasRest.DataContext
{
    public class InitialData
    {
        public static void Init(VentasContext context)
        {
            //Datos en memoria
            if(context.Database.ProviderName != "Microsoft.EntityFrameworkCore.InMemory")
            {
                return;
            }
            //Db cargada
            context.Database.EnsureCreated();
            var cliente = new Cliente[]
            {
                new Cliente{Id = 9, Nombre = "Ichika"},
                new Cliente{Id = 7, Nombre = "Ichika"},
            };
            foreach (Cliente c in cliente)
            {
                context.Clientes.AddAsync(c);
            }
        }
    }
}