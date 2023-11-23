using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Teste_Tecnico.Models;

namespace Teste_Tecnico.Data
{
    public class Teste_TecnicoContext : DbContext
    {
        public Teste_TecnicoContext (DbContextOptions<Teste_TecnicoContext> options)
            : base(options)
        {
        }

        public DbSet<Teste_Tecnico.Models.ClienteModel> ClienteModel { get; set; } = default!;
        public DbSet<Teste_Tecnico.Models.EnderecoModel> EnderecoModel { get; set; } = default!;
    }
}
