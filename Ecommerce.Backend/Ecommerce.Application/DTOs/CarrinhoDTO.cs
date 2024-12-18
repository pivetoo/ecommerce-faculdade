﻿using Ecommerce.Dominio.Entities;

namespace Ecommerce.Application.DTOs
{
    public class CarrinhoDTO
    {
        public long UsuarioId { get; set; }
        public long ProdutoId { get; set; }
        public Produto Produto { get; set; }
        public int Quantidade { get; set; }
    }
}
