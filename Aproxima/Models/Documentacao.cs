using System;
using System.Collections.Generic;

namespace Aproxima.Models
{
    public partial class Documentacao
    {
        public Documentacao()
        {
            SolicitacaoDocumentacao = new HashSet<SolicitacaoDocumentacao>();
        }

        public int Id { get; set; }
        public int ClassificacaoId { get; set; }
        public string Descricao { get; set; }

        public virtual Classificacao Classificacao { get; set; }
        public virtual ICollection<SolicitacaoDocumentacao> SolicitacaoDocumentacao { get; set; }
    }
}
