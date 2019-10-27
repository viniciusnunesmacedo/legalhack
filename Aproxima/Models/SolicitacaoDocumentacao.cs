using System;
using System.Collections.Generic;

namespace Aproxima.Models
{
    public partial class SolicitacaoDocumentacao
    {
        public Guid Id { get; set; }
        public Guid SolicitacaoId { get; set; }
        public int DocumentacaoId { get; set; }

        public virtual Documentacao Documentacao { get; set; }
        public virtual Solicitacao Solicitacao { get; set; }
    }
}
