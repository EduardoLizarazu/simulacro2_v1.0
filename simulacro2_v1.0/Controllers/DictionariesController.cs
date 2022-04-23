using Microsoft.AspNetCore.Mvc;
using simulacro2_v1._0.Models;

namespace simulacro2_v1._0.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DictionariesController : Controller
    {
        private static List <Dictionary> _dictionaries = new();
        
        [HttpGet]
        public IEnumerable<Dictionary> DevolverArreglo()
        {
            return _dictionaries;
        }

        [HttpPost]
        [Route("addManyItems")]
        public void InsertarElemento(List<Dictionary> dictionary)
        {
            foreach(var item in dictionary)
            {
                InsertarElemento(item);
            }
        }
        [HttpPost]
        public IActionResult InsertarElemento(Dictionary dictionary)
        {
           if(Verificar(dictionary.Key))
           {
                return BadRequest();
           }
           _dictionaries.Add(dictionary);
            return Ok();
        }

        [HttpGet]
        [Route("search")]
        public Dictionary BuscarElemento(string key)
        {
            foreach (var dic in _dictionaries)
            {
                if (dic.Key == key)
                    return dic;
            }
            return new Dictionary { Key="No encontrado", Value=-1 };
        }
        [HttpGet]
        [Route("plus")]
        public Dictionary SumarElementos()
        {
            double counter = 0;
            foreach (var dic in _dictionaries)
            {
                counter += dic.Value;
            }
            return new Dictionary { Key = "La suma de todos los Values", Value = counter };
        }
        [HttpGet]
        [Route("randomItem")]
        public Dictionary RandomElemento()
        {
            var randomNumber = new Random().Next(0, _dictionaries.Count);
            return _dictionaries[randomNumber];
        }


        [HttpPut]
        public IActionResult ModificarElemento(Dictionary dictionary)
        {
            foreach (var dic in _dictionaries)
            {
                if (dic.Key == dictionary.Key)
                {
                    dic.Value = dictionary.Value;
                    return Ok();
                }
            }
            return  NotFound();
        }


        [HttpDelete]
        public IActionResult EliminarElemento(string key)
        {
            int post = 0;
            foreach (var dic in _dictionaries)
            {
                if (dic.Key == key)
                {
                    _dictionaries.RemoveAt(post);
                    return Ok();
                }
                post++;
            }
            return NotFound();
        }

        private bool Verificar(string key)
        {
            foreach(var dic in _dictionaries)
            {
                if(dic.Key == key)
                    return true;
            }
            return false;
        }
    }
}
