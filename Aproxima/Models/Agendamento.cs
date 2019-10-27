using System;
using System.Collections.Generic;

namespace Aproxima.Models
{
    public partial class Agendamento
    {
        public Guid Id { get; set; }
        public Guid SolicitacaoId { get; set; }
        public DateTime Data { get; set; }
        public TimeSpan Hora { get; set; }

        public virtual Solicitacao Solicitacao { get; set; }
    }
}
