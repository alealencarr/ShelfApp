﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shelf.Core.Requests.Categories
{
    public class CreateCategoryRequest : Request 
    {

        [Required(ErrorMessage = "Titulo inválido.")]
        [MaxLength(80, ErrorMessage = "O titulo deve conter, no máximo, 80 caracteres.")]
        public string Title = string.Empty;

        [Required(ErrorMessage = "Descrição inválida.")]
        public string Description = string.Empty;
    }
}
