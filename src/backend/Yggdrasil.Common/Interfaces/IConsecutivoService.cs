namespace Yggdrasil.Common.Interfaces
{
    public interface IConsecutivoService
    {
        Task<(bool Success, int ConsecutivoGenerado, string ErrorMessage)> ObtenerSiguienteConsecutivoAsync(
            string nombreTabla,
            CancellationToken cancellationToken = default);

        /// <summary>
        /// Obtiene el consecutivo actual sin incrementarlo
        /// Útil para consultas
        /// </summary>
        Task<int> ObtenerConsecutivoActualAsync(string nombreTabla, CancellationToken cancellationToken = default);

        /// <summary>
        /// Inicializa un consecutivo para una tabla si no existe
        /// </summary>
        Task<bool> InicializarConsecutivoAsync(string nombreTabla, int valorInicial = 1, CancellationToken cancellationToken = default);

        /// <summary>
        /// Reinicia el consecutivo de una tabla (usar con precaución)
        /// </summary>
        Task<bool> ReiniciarConsecutivoAsync(string nombreTabla, int nuevoValor = 1, CancellationToken cancellationToken = default);
    }
}
