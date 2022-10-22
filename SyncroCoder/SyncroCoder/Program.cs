using SyncroCoder;
using SyncroCoder.Models;
using SyncroCoder.Repository;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Reflection.Emit;
using System.Runtime.CompilerServices;
using static System.Net.Mime.MediaTypeNames;
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddCors(policy =>
{
    policy.AddDefaultPolicy(option =>
    option.AllowAnyHeader().AllowAnyOrigin().AllowAnyMethod());
});

var app = builder.Build();

app.UseCors();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();


/*class Program

{
    static void Main(string[] args)
    {


        //        //Usuario.GetUsuario();

        //        //Producto.GetProducto();

        //        //ProductoVendido.GetProductoVendido();

        //        //Venta.GetVenta();

        //        //Usuario.Login();



        //        //ADO_Producto.UpdateProducto();

        //        //ADO_Producto.EliminarProducto();

        //        //ADO_Producto.GetProductoFromDatabase(2);


        //        /*DESAFIO ENTREGABLE*/

        //        //Id 1002 
        //        //ADO_Usuario.UpdateUsuario(1002, "Hernan", "Lopez", "hlopez", "5649", "hlopez@mail.com");

        //        //Crea el producto con los parametros indicados
        //        //ADO_Producto.CrearProducto("Cerveza", 120, 170, 1000, 1); 

        //        //Updetea el producto con los parametos que tiene el metodo
        //        //ADO_Producto.UpdateProducto(1002, "Lata de cerveza", 130, 180, 1010, 1);

        //        //Elimina el producto con el id pasado por parametro
        //        //ADO_Producto.EliminarProducto(1002);


   // }
//}
