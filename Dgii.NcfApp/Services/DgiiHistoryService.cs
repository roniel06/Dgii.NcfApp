using System;
using Dgii.NcfApp.Context;
using Dgii.NcfApp.Models;
using Microsoft.EntityFrameworkCore;

namespace Dgii.NcfApp.Services
{
    public interface IDgiiHistoryService
    {
        Task<bool> CreateNcfHistory(NcfResult ncfResult);
        Task<bool> CreatePlacaHistory(GetPlacaResponse placasResult);
        Task<IEnumerable<NcfResultHistory>> GetNcfHistory();
        //Task<IEnumerable<Placa>> GetPlacasHistory();

    }

    public class DgiiHistoryService : IDgiiHistoryService
    {
        private ProjectDbContext _context;
        public DgiiHistoryService(ProjectDbContext context)
        {
            _context = context;
        }

        public async Task<bool> CreateNcfHistory(NcfResult ncfResult)
        {
            try
            {
                var entity = new NcfResultHistory
                {
                    Comprobante = ncfResult.Comprobante,
                    Estado = ncfResult.Estado,
                    Nombre = ncfResult.Nombre,
                    Ncf = ncfResult.Ncf,
                    Rnc = ncfResult.Rnc,
                    FechaVigencia = ncfResult.FechaVigencia,
                    FechaCreacion = DateTime.Now,
                    MensajeValidacion = ncfResult.MensajeValidacion,
                    EsValido = ncfResult.EsValido


                };

                await _context.NcfResultHistory.AddAsync(entity);
                return await _context.SaveChangesAsync() > 0;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public async Task<bool> CreatePlacaHistory(GetPlacaResponse placasResult)
        {
            var placaResult = new PlacasResultHistory
            {
                Placa = placasResult.GetPlacaResult.placa.PLACA,
                MarcaVehiculo = placasResult.GetPlacaResult.placa.MARCA_VEHICULO,
                ModeloVehiculo = placasResult.GetPlacaResult.placa.MODELO_VEHICULO,
                Color = placasResult.GetPlacaResult.placa.COLOR,
                AnioFabricacion = placasResult.GetPlacaResult.placa.ANO_FABRICACION,
                RncCedulaPropietario = placasResult.GetPlacaResult.placa.RNC_CEDULA_PROPIETARIO,
                FechaCreacion = DateTime.Now
            };
            await _context.AddAsync(placaResult);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<IEnumerable<NcfResultHistory>> GetNcfHistory()
        {
            var result = await _context.NcfResultHistory.AsNoTracking().ToListAsync();
            if (result is not null)
            {
                return result;
            }

            else { return new List<NcfResultHistory>(); }
        }

        public async Task<IEnumerable<string>> GetPlacasHistory()
        {
            //var result = await _context.PlacasResultHistory.ToListAsync();
            //if(result is not null)
            //{
            //    return result;
            //}
            //else { return new List<Placa>(); }
            return null;

        }
    }
}

