using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using ProjectMVC.DTOs;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace ProjectMVC.Controllers
{
    public class AuthorController : Controller
    {
        public async Task<IActionResult> Index()
        {

            AuthorListDto listDto;

            using (HttpClient client = new HttpClient())
            {


                var response = await client.GetAsync("https://localhost:44305/admin/api/authors");

                var responseStr = await response.Content.ReadAsStringAsync();

                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    listDto = JsonConvert.DeserializeObject<AuthorListDto>(responseStr);
                    return View(listDto);

                }
            }


            return RedirectToAction("index", "home");
        }

        //[HttpPost]
        //public async Task<IActionResult> Create(AuthorPostDto postDto)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return View();
        //    }
        //    using (HttpClient client = new HttpClient())
        //    {
        //        byte[] byteArr = null;

        //        if (postDto.ImageFile.Length > 0)
        //        {
        //            using (var mStream = new MemoryStream())
        //            {
        //                postDto.ImageFile.CopyTo(mStream);
        //                byteArr = mStream.ToArray();
        //            }
        //        }
        //        else
        //        {
        //            return BadRequest();
        //        }

        //        var byteArrContent = new ByteArrayContent(byteArr);
        //        byteArrContent.Headers.ContentType = MediaTypeHeaderValue.Parse(postDto.ImageFile.ContentType);
        //        var multiPartContent = new MultipartFormDataContent();
        //        multiPartContent.Add(new StringContent(JsonConvert.SerializeObject(postDto.Name), Encoding.UTF8, "application/json"), "Name");
        //        multiPartContent.Add(byteArrContent, "ImageFile", postDto.ImageFile.FileName);


        //        string endPoint = "https://localhost:44305/admin/api/authors";
        //        using (var response = await client.PostAsync(endPoint, multiPartContent))
        //        {
        //            if (response.StatusCode == System.Net.HttpStatusCode.NoContent)
        //            {
        //                return RedirectToAction(nameof(Index));
        //            }
        //            else
        //            {
        //                return BadRequest();
        //            }
        //        }
        //    }
        //}

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(AuthorPostDto authorDto)
        {
            if (!ModelState.IsValid) return View();
            using (HttpClient client = new HttpClient())
            {
                byte[] byteArr = null;

                if (authorDto.ImageFile != null)
                {
                    using (var mStream = new MemoryStream())
                    {
                        authorDto.ImageFile.CopyTo(mStream);
                        byteArr = mStream.ToArray();
                    }
                }
                else
                {
                    return BadRequest();
                }
                var byteArrContent = new ByteArrayContent(byteArr);
                byteArrContent.Headers.ContentType = MediaTypeHeaderValue.Parse(authorDto.ImageFile.ContentType);
                var multipartContent = new MultipartFormDataContent();
                multipartContent.Add(new StringContent(JsonConvert.SerializeObject(authorDto.Name.Replace("\"", " ")), Encoding.UTF8, "application/json"), "Name");
                multipartContent.Add(byteArrContent, "ImageFile", authorDto.ImageFile.FileName);
                string endpoint = "https://localhost:44305/admin/api/authors";
                using (var Response = await client.PostAsync(endpoint, multipartContent))
                {
                    if (Response.StatusCode == System.Net.HttpStatusCode.Created)
                    {
                        return RedirectToAction("index", "author");
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

            AuthorPostDto postDto;

            using (HttpClient client = new HttpClient())
            {
                var response = await client.GetAsync("https://localhost:44305/admin/api/authors/" + id);

                var responseStr = await response.Content.ReadAsStringAsync();

                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    postDto = JsonConvert.DeserializeObject<AuthorPostDto>(responseStr);
                    return View(postDto);

                }
                else
                {
                    return NotFound();
                }
            }


        }

        [HttpPost]
        public async Task<IActionResult> Update(AuthorPostDto postDto)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }


            using (HttpClient client = new HttpClient())
            {
                byte[] byteArr = null;


                using (var mStream = new MemoryStream())
                {
                    postDto.ImageFile.CopyTo(mStream);
                    byteArr = mStream.ToArray();
                }

                var byteArrContent = new ByteArrayContent(byteArr);
                byteArrContent.Headers.ContentType = MediaTypeHeaderValue.Parse(postDto.ImageFile.ContentType);
                var multiPartContent = new MultipartFormDataContent();
                multiPartContent.Add(new StringContent(JsonConvert.SerializeObject(postDto.Name), Encoding.UTF8, "application/json"), "Name");
                multiPartContent.Add(byteArrContent, "ImageFile", postDto.ImageFile.FileName);


                string endPoint = "https://localhost:44305/admin/api/authors/" + postDto.Id;
                using (var response = await client.PutAsync(endPoint, multiPartContent))
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
                var response = await client.DeleteAsync("https://localhost:44305/admin/api/authors/" + id);

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
