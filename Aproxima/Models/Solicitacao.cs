using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Aproxima.Models
{
    public partial class Solicitacao
    {
        public Solicitacao()
        {
            Agendamento = new HashSet<Agendamento>();
            SolicitacaoDocumentacao = new HashSet<SolicitacaoDocumentacao>();
            SolicitacaoFornecedor = new HashSet<SolicitacaoFornecedor>();
        }

        public Guid? Id { get; set; }
        [Display(Name = "Classificação")]
        public int ClassificacaoId { get; set; }
        public string Detalhamento { get; set; }
        
        [Display(Name ="Renda Mensal")]
        public decimal? RendaMensal { get; set; }

        [Display(Name = "Quantos adultos moram com você")]
        public int? AdultosMoramComVoce { get; set; }

        [Display(Name = "Os adultos tem renda ?")]
        public bool AdultosTemRenda { get; set; }

        [Display(Name = "Total da renda mensal dos adultos")]
        public decimal? AdultosRenda { get; set; }

        public string Email { get; set; }
        public string Telefone { get; set; }

        [NotMapped]
        public string ClassificacaoDescricao { get; set; }

        public virtual Classificacao Classificacao { get; set; }
        public virtual ICollection<Agendamento> Agendamento { get; set; }
        public virtual ICollection<SolicitacaoDocumentacao> SolicitacaoDocumentacao { get; set; }
        public virtual ICollection<SolicitacaoFornecedor> SolicitacaoFornecedor { get; set; }
    }
}
