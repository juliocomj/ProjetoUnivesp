using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProjetoUNIVESP.Estudantes
{
    public class EstudanteMap
    {
        [Key]
        [Column(TypeName = "varchar(36)")]
        public Guid Id { get; init; }

        public string Nome { get; private set; }

        public bool Ativo { get; private set; }
        public string Local { get; private set; }
        public string Contato { get; private set; }
        public string Linkedin { get; private set; }
        public string Materia { get; private set; }
        public string Link { get; private set; }
        public string Topicos { get; private set; }
        public string DataHoraMentoria { get; private set; }

        public EstudanteMap(string nome, string local, string contato, string linkedin, string materia, string link, string topicos, string dataHoraMentoria)
        {
            Nome = nome;
            Id = Guid.NewGuid();
            Ativo = true;
            Materia = materia;
            Local = local;
            Contato = contato;
            Linkedin = linkedin;
            Link = link;
            Topicos = topicos;
            DataHoraMentoria = dataHoraMentoria;
        }

        public void AtualizarNome(string nome, string local, string contato, string linkedin, string materia, string link, string topicos, string dataHoraMentoria)
        {
            Nome = nome;
            Local = local;
            Materia = materia;
            Contato = contato;
            Linkedin = linkedin;
            Link = link;
            Topicos = topicos;
            DataHoraMentoria = dataHoraMentoria;
        }

        public void Desativar()
        {
            Ativo = false;
        }
    }

}
