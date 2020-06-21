using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

using WebLocadora.Models;

namespace WebLocadora
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<ApiDbContext>(options => options.UseInMemoryDatabase("LocadoraDB"));

            services.AddMvc().AddJsonOptions(ConfJSON);

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
        }

        private void ConfJSON(MvcJsonOptions obj)
        {
            obj.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ApiDbContext context)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseMvc();

            if (!context.Filmes.Any())
            {
                context.Filmes.AddRange(new List<Filme>() {
                    new Filme(){NomeFilme = "E o Vento Levou", Sinopse="Filme dos anos 60 com historia de uma garotinha que se perde em meio a um tornado e é transportada para um mundo de fantasias", valorLocacao=1.75M, Locado = true, Ativo = true},
                    new Filme(){NomeFilme = "Maquina Mortifera", Sinopse="Filme dos anos 90 com historia de um policial psicotico que busca reconciliação com sua esposa e se vê em meio de uma garnde confusão.", valorLocacao=2.0M, Locado = false, Ativo = true},
                    new Filme(){NomeFilme = "Duro de Matar", Sinopse="Filme dos anos 90 com historia de um policial que tenta impedir seu divórcio e impedir um grande assalto", valorLocacao=2.5M, Locado = false,Ativo = true},
                    new Filme(){NomeFilme = "Matrix", Sinopse="Filme dos anos 2000 com historia futurista onde pessoas são controladas por máquinas e são usadas como fonte de energi apara manter a sociedade das maquinas", valorLocacao=2.5M, Locado = true, Ativo = true},
                    new Filme(){NomeFilme = "Falção Azul", Sinopse="Filme dos anos 80 com historia de uma equipe de pilotos de helicopteros que precisam impedir uma guerra mundial", valorLocacao=4.0M, Locado = false, Ativo = true },
                    new Filme(){NomeFilme = "Alladin", Sinopse="Filme dos anos90 com historia de um menino pobre nas ruas de un califado no deserto que precisa conquistar a linda princesa.", valorLocacao=2.5M, Locado = true, Ativo = true},
                    new Filme(){NomeFilme = "Aterix e Obelix", Sinopse="Filme dos anos 60 com as Aventuras deos Galeses mais loucos do Imperio româno", valorLocacao=3.5M, Locado = false, Ativo = true},
                    new Filme(){NomeFilme = "Todo Mundo em Pânico", Sinopse="Filme dos anos 2005 com historia de terror e suspense que deixará todo mundo em pânico", valorLocacao=1.5M, Locado = true, Ativo = true},
                    new Filme(){NomeFilme = "As branquelas", Sinopse="Filme dos anos 2002 com historia de uma dupla de agentes do FBI para lá de malucos e atrapalhados", valorLocacao=5.5M, Locado = false, Ativo = true},
                    new Filme(){NomeFilme = "A Cabana", Sinopse="Filme de auto ajuda que reforça a sua espiritualidade e prega o amor de Deus aos homens", valorLocacao=2.5M, Locado = false, Ativo = true},
                    new Filme(){NomeFilme = "A Lagoa Azul", Sinopse="Um grupo de naufragos encontram uma ilha paradisiaca onde a beleza é o senári para diversas aventuras", valorLocacao=2.8M, Locado = false, Ativo = true},
                    new Filme(){NomeFilme = "Batman Begins", Sinopse="Um fileme que conta como um garoto rico se tranforma em um vigilânte  e justiçeiro nas ruas de Gothan", valorLocacao=4.0M, Locado = false, Ativo = true },
                    new Filme(){NomeFilme = "Dr. Aloprado", Sinopse="Comédia com Ed Marphin que conta a luta de um profesor gordo para ser magro e respeitado em uma universidade", valorLocacao=3.25M, Locado = false, Ativo = true},
                    new Filme(){NomeFilme = "E o Vento Levou", Sinopse="Filme dos anos 60 com historia de uma garotinha que se perde em meio a um tornado e é transportada para um mundo de fantasias", valorLocacao=1.75M, Locado = false, Ativo = true},

                });

                context.SaveChanges();

                if (!context.Clientes.Any())
                {
                    context.Clientes.AddRange(new List<Cliente>() {
                    new Cliente(){ Nome="Marcos Aurélio Furtado Coelho", CPF="111.111.111-11", Cidade="Piripiri", Bairro="Centro", Endereco= "Avenida Thomaz Rabelo", Numero=30, Telefone="(086)95432-2345", Ativo=true },
                    new Cliente(){ Nome="Marcelo Castro", CPF="222.222.222-22", Cidade="Piripiri", Bairro="São João", Endereco= "Avenida São João", Numero=137, Telefone="(086)95432-3456", Ativo=true },
                    new Cliente(){ Nome="Joana Elias Tavares", CPF="333.333.333-33", Cidade="Piripiri", Bairro="Anajas", Endereco= "Avenida Thomaz Rabelo", Numero=33, Telefone="(086)95432-2334", Ativo=true },
                    new Cliente(){ Nome="Messias Honório caldas", CPF="444.444.444-44", Cidade="Piripiri", Bairro="Floresta", Endereco= "Avenida Thomaz Rabelo", Numero=1034, Telefone="(086)95432-1122", Ativo=true },
                    new Cliente(){ Nome="Eloisa Alburquerque Saboia", CPF="555.555.555-55", Cidade="Piripiri", Bairro="Campo das Palmas", Endereco= "Alameda sempre verde", Numero=99, Telefone="(086)95432-2211", Ativo=true },
                    new Cliente(){ Nome="Helio Castro Neves", CPF="666.666.666-66", Cidade="Piripiri", Bairro="Petecas", Endereco= "Travessa das Orquideias", Numero=122, Telefone="(086)95432-2311", Ativo=true },
                    new Cliente(){ Nome="Manoel Cunha Machado", CPF="777.777.777-77", Cidade="Piripiri", Bairro="Fonte dos Matos", Endereco= "Rua Pedro Paulo de Andrdade", Numero=124, Telefone="(086)95432-3311", Ativo=true },
                    new Cliente(){ Nome="Arimatheia Mendes de Assunção", CPF="888.888.888-88", Cidade="Piripiri", Bairro="Germanno", Endereco= "Rua Sinha Sabóia", Numero=145, Telefone="(086)95432-2323", Ativo=true },
                    new Cliente(){ Nome="Paulo Henrique da Costa Portela", CPF="999.999.999-99", Cidade="Piripiri", Bairro="Cachoeira", Endereco= "Rua Castro Alves", Numero=233, Telefone="(086)95432-1222", Ativo=true },
                    new Cliente(){ Nome="Antônio Mendes da Silva Lima", CPF="000.000.000-00", Cidade="Piripiri", Bairro="Centro", Endereco= "Avenida Thomaz Rabelo", Numero=130, Telefone="(086)95432-1114", Ativo=true },
                   });

                    context.SaveChanges();
                }

                if (!context.Tickets.Any())
                {

                    context.Tickets.AddRange(new List<Ticket>() {
                        new Ticket() { idCliente = 1, Ativo = false},
                        new Ticket(){ idCliente = 2, Ativo = true }
                    });
                    context.SaveChanges();
                }

                if (!context.Locacoes.Any())
                {
                    context.Locacoes.AddRange(new List<Locacao>() {
                       new Locacao(){ idFilme = 1, dataLocacao = DateTime.Now.AddDays(0), dataDevolucao = DateTime.Now.AddDays(2), diasLocacao = 2, Ativo = false, idTicket = 1, valorLocacao = 5M },
                       new Locacao(){ idFilme = 4, dataLocacao = DateTime.Now.AddDays(0), dataDevolucao = DateTime.Now.AddDays(2), diasLocacao = 2, Ativo = false, idTicket = 1, valorLocacao = 3M },
                       new Locacao(){ idFilme = 6, dataLocacao = DateTime.Now.AddDays(-4), dataDevolucao = DateTime.Now.AddDays(-2), diasLocacao = 2, Ativo = true, idTicket = 2, valorLocacao = 4.6M },
                       new Locacao(){ idFilme = 8, dataLocacao = DateTime.Now.AddDays(0), dataDevolucao = DateTime.Now.AddDays(2), diasLocacao = 2, Ativo = false, idTicket = 1, valorLocacao = 2M }
                    });
                    context.SaveChanges();
                }
            }

        }
    }
}
