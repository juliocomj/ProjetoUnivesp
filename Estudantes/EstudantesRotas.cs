using ProjetoUNIVESP.Estudantes;
using ProjetoUNIVESP.Data;
using Microsoft.EntityFrameworkCore;

namespace ProjetoUNIVESP.Estudantes
{
    public static class EstudantesRotas
    {
        public static void AddRotasEstudantes(this WebApplication app)
        {
            var rotasEstudantes = app.MapGroup("estudantes");

            //CRIAR UM ESTUDANTE - metodo Post

            rotasEstudantes.MapPost("cadastro", async (AddEstudanteRequest request, AppDbContext context, CancellationToken ct) =>
            {
                var jaExiste = context.Estudantes.Any(estudante => estudante.Nome == request.Nome && estudante.DataHoraMentoria == request.DataHoraMentoria);
                if (jaExiste)
                {
                    return Results.Conflict("Já existe uma mentoria para essa pessoa nesse horário!");
                }
                
                var novoEstudante = new EstudanteMap(request.Nome, request.Local, request.Materia, request.Contato, request.Topicos, request.Link, request.Linkedin, request.DataHoraMentoria);

                await context.Estudantes.AddAsync(novoEstudante, ct);
                await context.SaveChangesAsync();

                var estudanteRetorno = new EstudanteDto(novoEstudante.Id, novoEstudante.Nome, novoEstudante.Local, novoEstudante.Contato, novoEstudante.Linkedin, novoEstudante.Materia, novoEstudante.Link, novoEstudante.Topicos, novoEstudante.DataHoraMentoria);

                return Results.Ok(estudanteRetorno);
            });

            //RETORNAR TODOS OS ESTUDANTES CADASTRADOS

            rotasEstudantes.MapGet("listagem", async (AppDbContext context, CancellationToken ct) =>
            {
                var estudantes = await context
                    .Estudantes
                    .Where(estudante => estudante.Ativo)
                    .Select(estudante => new EstudanteDto(estudante.Id, estudante.Nome, estudante.Local, estudante.Contato, estudante.Linkedin, estudante.Materia, estudante.Link, estudante.Topicos, estudante.DataHoraMentoria))
                    .ToListAsync(ct);

                return estudantes;
            });

            // Atualizar nome e curso do estudante
            rotasEstudantes.MapPut("{id:guid}", async (Guid id, AtualizarEstudante requestEstudante, AppDbContext context, CancellationToken ct) =>
            {
                var estudante = await context.Estudantes
                .SingleOrDefaultAsync(estudante => estudante.Id == id, ct); //rastreia todas as informações do item que foi selecionado e a partir dai executa as ações

                if(estudante == null)
                {
                    return Results.NotFound();
                }

                estudante.AtualizarNome(requestEstudante.Nome, requestEstudante.Local, requestEstudante.Contato, requestEstudante.Linkedin, requestEstudante.Materia, requestEstudante.Link, requestEstudante.Topicos, requestEstudante.DataHoraMentoria);

                await context.SaveChangesAsync(ct);
                return Results.Ok(new EstudanteDto(estudante.Id, estudante.Nome, estudante.Local, estudante.Contato, estudante.Linkedin, estudante.Materia, estudante.Link, estudante.Topicos, estudante.DataHoraMentoria));

            });

            //deletar um estudante (vamos apenas desativar ele no banco para não ser visivel na listagem)
            rotasEstudantes.MapDelete("{id}", async (Guid id, AppDbContext context, CancellationToken ct) =>
            {
                var estudante = await context.Estudantes.SingleOrDefaultAsync(estudante => estudante.Id == id, ct);

                if(estudante == null)
                {
                    return Results.NotFound();
                }

                estudante.Desativar();

                await context.SaveChangesAsync(ct);

                return Results.Ok();
            });

            //rotasEstudantes.MapPost("mentoria", async (AddMentoriaRequest request, AppDbContext context, CancellationToken ct) =>
            //{
            //    var jaTemMentoriaMarcada = context.Mentoria.Any(mentoria => mentoria.Data_Encontro == request.Data_Encontro && mentoria.Nome == request.Nome);
            //    if (jaTemMentoriaMarcada)
            //    {
            //        return Results.Conflict("Já existe uma mentoria marcada para esse dia!");
            //    }

            //    var novaMentoria = new MentoriaMap
            //    (request.Nome, request.Email, request.Curso, request.Data_Encontro, request.Local);

            //    await context.Mentoria.AddAsync(novaMentoria, ct);
            //    await context.SaveChangesAsync();

            //    var estudanteRetorno = new MentoriaDto(novaMentoria.Codigo, novaMentoria.Nome, novaMentoria.Curso, novaMentoria.Email, novaMentoria.Data_Encontro, novaMentoria.Local);

            //    return Results.Ok(estudanteRetorno);
            //});
        }
            
    }
}
