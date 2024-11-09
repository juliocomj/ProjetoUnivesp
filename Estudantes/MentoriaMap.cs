using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProjetoUNIVESP.Estudantes
{
    public class MentoriaMap
    {
        [Key]
        [Column(TypeName = "varchar(36)")]
        public Guid Codigo { get; init; }

        public string Nome { get; private set; }

        public string Email { get; private set; }
        public string Curso { get; private set; }
        public string Data_Encontro { get; private set; }
        public string Local { get; private set; }
        public MentoriaMap(string nome, string email, string curso,  string data_Encontro, string local)
        {
            Nome = nome;
            Codigo = Guid.NewGuid();
            Email = email;
            Curso = curso;
            Data_Encontro = data_Encontro;
            Local = local;
        }

        /*public void AtualizarNome(string nome, string curso)
        {
            Nome = nome;
            Curso = curso;
        }

        public void Desativar()
        {
            Ativo = false;
        }
        */
    }
}
