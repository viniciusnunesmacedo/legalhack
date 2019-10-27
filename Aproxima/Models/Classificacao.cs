using System;
using System.Collections.Generic;

namespace Aproxima.Models
{
    public partial class Classificacao
    {
        public Classificacao()
        {
            ClassificacaoFornecedor = new HashSet<ClassificacaoFornecedor>();
            Documentacao = new HashSet<Documentacao>();
            Solicitacao = new HashSet<Solicitacao>();
        }

        public int Id { get; set; }
        public int IdPai { get; set; }
        public string Descricao { get; set; }
        public string Orientacao { get; set; }
        public string Icone { get; set; }
        public string Cor { get; set; }

        public virtual ICollection<ClassificacaoFornecedor> ClassificacaoFornecedor { get; set; }
        public virtual ICollection<Documentacao> Documentacao { get; set; }
        public virtual ICollection<Solicitacao> Solicitacao { get; set; }
    }
}
