using GenericApp.Common.Requests;
using GenericApp.Common.Responses;
using System;
using System.IO;
using System.Threading.Tasks;

namespace GenericApp.Common.Services
{
    public interface IApiService
    {
        Task<ResponseT<UsuarioAppResponse>> GetUserByEmailAsync(
           string urlBase,
           string servicePrefix,
           string controller,
           string email,
           string password);

        Task<ResponseT<CausanteResponse>> GetCausanteByCodigoAsync(
           string urlBase,
           string servicePrefix,
           string controller,
           string codigo);

        Task<ResponseT<object>> GetEntregasForCodigo(
           string urlBase,
           string servicePrefix,
           string controller,
           string codigo);

        Task<ResponseT<object>> GetEntregas2ForCodigo(
          string urlBase,
          string servicePrefix,
          string controller,
          string codigo);

        Task<Response> GetEntregaDetallesPorFecha(
            string urlBase,
            string servicePrefix,
            string controller,
            EntregaDetallesRequest model);

        Task<ResponseT<object>> GetObras(
         string urlBase,
         string servicePrefix,
         string controller);

        Task<ResponseT<object>> GetCatalogos(
         string urlBase,
         string servicePrefix,
         string controller);

        Task<ResponseT<object>> GetObrasPoste(
        string urlBase,
        string servicePrefix,
        string controller,
        int id);

        Task<ResponseT<object>> GetNroRegistroMax(
         string urlBase,
         string servicePrefix,
         string controller);

        Task<ResponseT<ObrasPosteResponse>> GetTicketAsync(
           string urlBase,
           string servicePrefix,
           string controller,
           string codigo);

        Task<ResponseT<object>> GetObrasDocumentosAsync(
            string urlBase,
            string servicePrefix,
            string controller,
            int IDObrasPostes);

        Task<ResponseT<object>> PutAsync2<T>(
            string urlBase,
            string servicePrefix,
            string controller,
            T model,
            int id);

        Task<Response> GetListAsync<T>(string urlBase, string servicePrefix, string controller);

        Task<Response> GetTokenAsync(string urlBase, string servicePrefix, string controller, TokenRequest request);

        Task<Response> RegisterUserAsync(string urlBase, string servicePrefix, string controller, UserRequest userRequest);

        Task<Response> RecoverPasswordAsync(string urlBase, string servicePrefix, string controller, EmailRequest emailRequest);

        Task<Response> ModifyUserAsync(string urlBase, string servicePrefix, string controller, UserRequest userRequest, string token);

        Task<Response> ChangePasswordAsync(string urlBase, string servicePrefix, string controller, ChangePasswordRequest changePasswordRequest, string token);

        Task<Response> GetListAsync<T>(string urlBase, string servicePrefix, string controller, string token);

        Task<Stream> GetPictureAsync(string urlBase, string servicePrefix);

        Task<Response> PutAsync<T>(string urlBase, string servicePrefix, string controller, T model, string token);

        Task<ResponseT<object>> PutAsync<T>(
             string urlBase,
             string servicePrefix,
             string controller,
             int id,
             T model,
             string tokenType,
             string accessToken);

        Task<ResponseT<object>> PostAsync<T>(
          string urlBase,
          string servicePrefix,
          string controller,
          T model);

        Task<Response> DeleteAsync(
           string urlBase,
           string servicePrefix,
           string controller,
           int id);

        Task<Response> DeleteAsync(
           string urlBase,
           string servicePrefix,
           string controller,
           int id,
           string tokenType,
           string accessToken);
    }
}