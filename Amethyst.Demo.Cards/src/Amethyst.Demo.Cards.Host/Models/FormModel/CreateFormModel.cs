using System;

namespace Amethyst.Demo.Cards.Host.Models.FormModel
{
    public class CreateFormModel
    {
        public Guid CardId { get; set; }
        
        public Guid UserId { get; set; }
        
        public string Name { get; set; }
    }
}