using SistemaControleGastosResidenciais.DTOs.Requests;
using SistemaControleGastosResidenciais.DTOs.Responses;

namespace SistemaControleGastosResidenciais.Services.Interfaces {
    public interface IPersonService {
        PersonResponse FindById(Guid id);
        PagedResponse<PersonResponse> FindAll(
            int page,
            int pageSize
        );
        PersonResponse Create(CreatePersonRequest personDTO);
        void Delete(Guid id);
    }
}
