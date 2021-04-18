using AGENDA_BR.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System.IO;
using Microsoft.EntityFrameworkCore;

namespace AGENDA_BR.Controllers
{
    public class PessoasController : Controller
    {
        private readonly Contexto _contexto;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public PessoasController(Contexto contexto, IWebHostEnvironment webHostEnvironment)
        {
            _contexto = contexto;
            _webHostEnvironment = webHostEnvironment;
        }
        public async Task<IActionResult> IndexAsync()
        {
            return View(await _contexto.Pessoas.ToListAsync());
        }

        [HttpGet]
        public IActionResult AdicionarPessoa()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult>AdicionarPessoa(Pessoa pessoa, IFormFile foto)
        {
            if(ModelState.IsValid)
            {
                if(foto != null)
                {
                    string diretorio = Path.Combine(_webHostEnvironment.WebRootPath, "imagens");
                    string Fotonome = Guid.NewGuid().ToString() + foto.FileName;

                    using(FileStream fileStream = new FileStream(Path.Combine(diretorio,Fotonome), FileMode.Create))
                    {
                        await foto.CopyToAsync(fileStream);
                        pessoa.Foto = "~/imagens/" + Fotonome;
                    }
                }
                else
                {
                    pessoa.Foto = "~/imagens/kisspng-computer-icons-user-clip-art-user-5abf13db298934.2968784715224718991702.jpg";
                }
                _contexto.Add(pessoa);
                await _contexto.SaveChangesAsync();
                TempData["Novo Contato"] = $"Contato incluido com sucesso";
                return RedirectToAction(nameof(Index));
            }
            return View(pessoa);
        }
        [HttpGet]
        public async Task<IActionResult> AtualizarPessoa(int pessoaId)
        {
            Pessoa pessoa = await _contexto.Pessoas.FindAsync(pessoaId);

            if (pessoa == null)
                return NotFound();

            TempData["Foto"] = pessoa.Foto;
            return View(pessoa);
        }
        [HttpPost]
        public async Task<IActionResult>AtualizarPessoa(Pessoa pessoa, IFormFile foto)
        {
            if(ModelState.IsValid)
            {
               if(foto != null)
                {
                    string diretorio = Path.Combine(_webHostEnvironment.WebRootPath, "imagens");
                    string Fotonome = Guid.NewGuid().ToString() + foto.FileName;
                    using(FileStream fileStream = new FileStream(Path.Combine(diretorio, Fotonome),FileMode.Create))
                    {
                        await foto.CopyToAsync(fileStream);
                        pessoa.Foto = "~/imagens/" + Fotonome;
                        TempData["Foto"] = null;
                    }
                }
               else
                {
                    pessoa.Foto = TempData["Foto"].ToString();
                }
                _contexto.Update(pessoa);
                await _contexto.SaveChangesAsync();
                TempData["ContatoAtualizado"] = "atualizado com sucesso";
                return RedirectToAction(nameof(Index));
            }
            return View(pessoa);
        }

        [HttpPost]
        public async Task<JsonResult> ExcluirPessoa(int pessoaId)
        {
            Pessoa pessoa = await _contexto.Pessoas.FindAsync(pessoaId);
            _contexto.Pessoas.Remove(pessoa);
            await _contexto.SaveChangesAsync();
            TempData["ContatoExcluido"] = $"Contato {pessoa.Nome} {pessoa.Sobrenome} excluído com sucesso";
            return Json(true);
        }
    }
}
