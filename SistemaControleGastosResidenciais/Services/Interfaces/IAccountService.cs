using SistemaControleGastosResidenciais.DTOs.Requests;
using SistemaControleGastosResidenciais.DTOs.Responses;

namespace SistemaControleGastosResidenciais.Services.Interfaces {
    public interface IAccountService {
        AccountResponse FindById(Guid id);
        AccountResponse Create(CreateAccountRequest accountDTO);
        void Delete(Guid id);
    }
}