using System;
using System.Collections.Generic;

namespace Aproxima.Models
{
    public partial class SolicitacaoFornecedor
    {
        public Guid Id { get; set; }
        public Guid SolicitacaoId { get; set; }
        public int FornecedorId { get; set; }

        public virtual Fornecedor Fornecedor { get; set; }
        public virtual Solicitacao Solicitacao { get; set; }
    }
}
