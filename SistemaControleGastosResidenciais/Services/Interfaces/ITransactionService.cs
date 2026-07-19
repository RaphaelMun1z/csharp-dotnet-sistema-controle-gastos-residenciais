using SistemaControleGastosResidenciais.DTOs.Requests;
using SistemaControleGastosResidenciais.DTOs.Responses;

namespace SistemaControleGastosResidenciais.Services.Interfaces {
    public interface ITransactionService {
        TransactionResponse FindById(Guid id);
        PagedResponse<TransactionResponse> FindAll(
            int page,
            int pageSize
        );
        PagedResponse<TransactionResponse> FindByPersonId(
            Guid personId,
            int page,
            int pageSize
        );
        TransactionResponse Create(CreateTransactionRequest transactionDTO);
    }
}
