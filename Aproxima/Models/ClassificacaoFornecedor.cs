using System;
using System.Collections.Generic;

namespace Aproxima.Models
{
    public partial class ClassificacaoFornecedor
    {
        public int Id { get; set; }
        public int ClassificacaoId { get; set; }
        public int FornecedorId { get; set; }

        public virtual Classificacao Classificacao { get; set; }
        public virtual Fornecedor Fornecedor { get; set; }
    }
}
