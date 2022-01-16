using System;
using AutoMapper;
using ControleFinanceiro.Domain.Models;
using ControleFinanceiro.WebApi.Dtos.Balanco;
using ControleFinanceiro.WebApi.Dtos.Categoria;
using ControleFinanceiro.WebApi.Dtos.Lancamento;
using ControleFinanceiro.WebApi.Dtos.SubCategoria;

namespace ControleFinanceiro.WebApi
{
	public class WebApiAutoMapperProfile : Profile
	{
		public WebApiAutoMapperProfile()
		{
			//Categoria
			CreateMap<CategoriaEntity, RetornaCategoriaGet>().ReverseMap();
			CreateMap<IncluiCategoriaPost, CategoriaEntity>().ReverseMap();
			CreateMap<AtualizaCategoriaPut, CategoriaEntity>().ReverseMap();

			//SubCategoria
			CreateMap<SubCategoriaEntity, RetornaSubCategoriaGet>().ReverseMap();
			CreateMap<IncluiSubCategoriaPost, SubCategoriaEntity>().ReverseMap();
			CreateMap<AtualizaSubCategoriaPut, SubCategoriaEntity>().ReverseMap();

			//Lancamento
			CreateMap<LancamentoEntity, LancamentoOutput>().ReverseMap();
			CreateMap<LancamentoInput, LancamentoEntity>().ReverseMap();

			//Balanco
			CreateMap<Balanco, BalancoOutput>().
				ForMember(destino=> destino.categoria
				,opt=> opt.MapFrom(origem=> origem.categoria)).ReverseMap();
			CreateMap<BalancoInput, BalancoFiltro>().ReverseMap();
		}
	}
}

