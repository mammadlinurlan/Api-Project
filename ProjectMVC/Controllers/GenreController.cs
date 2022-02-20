using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using ProjectMVC.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace ProjectMVC.Controllers
{
    public class GenreController : Controller
    {
        public async Task<IActionResult> Index()
        {

            GenreListDto listDto;

            using (HttpClient client = new HttpClient())
            {


                var response = await client.GetAsync("https://localhost:44305/admin/api/genres");

                var responseStr = await response.Content.ReadAsStringAsync();

                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    listDto = JsonConvert.DeserializeObject<GenreListDto>(responseStr);
                    return View(listDto);

                }
            }


            return RedirectToAction("index","home");
        }

        public async Task<IActionResult> Create(GenrePostDto postDto)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            using (HttpClient client = new HttpClient())
            {
                StringContent content = new StringContent(JsonConvert.SerializeObject(postDto), Encoding.UTF8, "application/json");
                string endPoint = "https://localhost:44305/admin/api/genres";
                using (var response = await client.PostAsync(endPoint, content))
                {
                    if (response.StatusCode == System.Net.HttpStatusCode.Created)
                    {
                        return RedirectToAction(nameof(Index));
                    }
                    else
                    {
                        return BadRequest();
                    }
                }
            }
        }


        public async Task<IActionResult> Update(int id)
        {
            GenrePostDto postDto;

            using (HttpClient client = new HttpClient())
            {


                var response = await client.GetAsync("https://localhost:44305/admin/api/genres/" + id);

                var responseStr = await response.Content.ReadAsStringAsync();

                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    postDto = JsonConvert.DeserializeObject<GenrePostDto>(responseStr);
                    return View(postDto);
                }
                else
                {
                    return NotFound();
                }
            }

        }

        [HttpPost]
        public async Task<IActionResult> Update(GenrePostDto postDto)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            using (HttpClient client = new HttpClient())
            {
                StringContent content = new StringContent(JsonConvert.SerializeObject(postDto), Encoding.UTF8, "application/json");
                string endPoint = "https://localhost:44305/admin/api/genres/" + postDto.Id;
                using (var response = await client.PutAsync(endPoint, content))
                {
                    if (response.StatusCode == System.Net.HttpStatusCode.NoContent)
                    {
                        return RedirectToAction(nameof(Index));
                    }
                    else
                    {
                        return BadRequest();
                    }
                }
            }
        }




        public async Task<IActionResult> Delete(int id)
        {
            using (HttpClient client = new HttpClient())
            {
                var response = await client.DeleteAsync("https://localhost:44305/admin/api/genres/" + id);

                var responseStr = await response.Content.ReadAsStringAsync();

                if (response.StatusCode == System.Net.HttpStatusCode.NoContent)
                {
                    return Json(new { status = 200 });
                }
                else
                {
                    return NotFound();
                }
            }
        }

    }
}
