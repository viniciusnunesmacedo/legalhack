using System;
using System.Collections.Generic;

namespace Aproxima.Models
{
    public partial class Fornecedor
    {
        public Fornecedor()
        {
            ClassificacaoFornecedor = new HashSet<ClassificacaoFornecedor>();
            SolicitacaoFornecedor = new HashSet<SolicitacaoFornecedor>();
        }

        public int Id { get; set; }
        public string Descricao { get; set; }

        public virtual ICollection<ClassificacaoFornecedor> ClassificacaoFornecedor { get; set; }
        public virtual ICollection<SolicitacaoFornecedor> SolicitacaoFornecedor { get; set; }
    }
}
