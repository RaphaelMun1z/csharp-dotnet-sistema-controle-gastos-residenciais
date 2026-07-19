using SistemaControleGastosResidenciais.DTOs.Requests;
using SistemaControleGastosResidenciais.DTOs.Responses;

namespace SistemaControleGastosResidenciais.Services.Interfaces {
    public interface IPersonService {
        PersonResponse FindById(Guid id);
        List<PersonResponse> FindAll();
        PersonResponse Create(CreatePersonRequest personDTO);
        void Delete(Guid id);
    }
}
