using Expenses.Application.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Expenses.Application.Command.Ai
{
    public class AnalyzeBudgetCommandHandler : IRequestHandler<AnalyzeBudgetCommand, string>
    {
        private readonly IAiService _aiService;

        public AnalyzeBudgetCommandHandler(IAiService aiService)
        {
            _aiService = aiService;
        }

        public async Task<string> Handle(AnalyzeBudgetCommand request,CancellationToken cancellationToken)
        {
            var prompt = $@"Пользователь имеет бюджет: Баланс: {request.Balance} Аренда: {request.Rent} Коммуналка: {request.Utilities} Прочие расходы:
                {string.Join(", ", request.ExtraCategories.Select(c => $"{c.Name}: {c.Amount}"))} Сделай краткий анализ бюджета, оцени финансовое состояние и дай рекомендации.";

            return await _aiService.GenerateAsync(prompt);
        }
    }
}
