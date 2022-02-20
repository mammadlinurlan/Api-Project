using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using ProjectMVC.DTOs;
using ProjectMVC.ViewModels;
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
    public class BookController : Controller
    {
        public async  Task<IActionResult> Index()
        {
            BookListDto listDto;

            using (HttpClient client = new HttpClient())
            {


                var response = await client.GetAsync("https://localhost:44305/admin/api/books");

                var responseStr = await response.Content.ReadAsStringAsync();

                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    listDto = JsonConvert.DeserializeObject<BookListDto>(responseStr);
                    return View(listDto);

                }
            }


            return RedirectToAction("index", "home");
        }
        public async Task<IActionResult> Create()
        {
            AuthorListDto authorListDto;
            GenreListDto genreListDto;

            var authorUrl = "https://localhost:44305/admin/api/authors";
            var genreUrl = "https://localhost:44305/admin/api/genres";

            using (HttpClient client = new HttpClient())
            {
                var AuthorResponse = await client.GetAsync(authorUrl);

                var AuthorResponseStr = await AuthorResponse.Content.ReadAsStringAsync();

                var GenreResponse = await client.GetAsync(genreUrl);

                var GenreResponseStr = await GenreResponse.Content.ReadAsStringAsync();
                if (GenreResponse.StatusCode == System.Net.HttpStatusCode.OK && AuthorResponse.StatusCode==System.Net.HttpStatusCode.OK)
                {
                    authorListDto = JsonConvert.DeserializeObject<AuthorListDto>(AuthorResponseStr);
                    genreListDto = JsonConvert.DeserializeObject<GenreListDto>(GenreResponseStr);

                    BookPostVM postVM = new BookPostVM
                    {
                        Authors = authorListDto,
                        Genres = genreListDto
                    };
                    return View(postVM);
                }
                else
                {
                    return BadRequest();
                }
            }
        }




        [HttpPost]
        public async Task<IActionResult> Create(BookPostVM bookVM)
        {
            if (!ModelState.IsValid) return View();
            using (HttpClient client = new HttpClient())
            {
                byte[] byteArr = null;

                if (bookVM.Books.ImageFile != null)
                {
                    using (var mStream = new MemoryStream())
                    {
                        bookVM.Books.ImageFile.CopyTo(mStream);
                        byteArr = mStream.ToArray();
                    }
                }
                else
                {
                    return BadRequest();
                }
                var byteArrContent = new ByteArrayContent(byteArr);
                byteArrContent.Headers.ContentType = MediaTypeHeaderValue.Parse(bookVM.Books.ImageFile.ContentType);
                var multipartContent = new MultipartFormDataContent();
                multipartContent.Add(byteArrContent, "ImageFile", bookVM.Books.ImageFile.FileName);
                multipartContent.Add(new StringContent(JsonConvert.SerializeObject(bookVM.Books.Name), Encoding.UTF8, "application/json"), "Name");
                multipartContent.Add(new StringContent(JsonConvert.SerializeObject(bookVM.Books.Language), Encoding.UTF8, "application/json"), "language");
                multipartContent.Add(new StringContent(JsonConvert.SerializeObject(bookVM.Books.CostPrice), Encoding.UTF8, "application/json"), "costprice");
                multipartContent.Add(new StringContent(JsonConvert.SerializeObject(bookVM.Books.SalePrice), Encoding.UTF8, "application/json"), "saleprice");
                multipartContent.Add(new StringContent(JsonConvert.SerializeObject(bookVM.Books.PageCount), Encoding.UTF8, "application/json"), "pagecount");
                multipartContent.Add(new StringContent(JsonConvert.SerializeObject(bookVM.Books.GenreId), Encoding.UTF8, "application/json"), "genreid");
                multipartContent.Add(new StringContent(JsonConvert.SerializeObject(bookVM.Books.AuthorId), Encoding.UTF8, "application/json"), "authorid");

                string endpoint = "https://localhost:44305/admin/api/books";
                using (var Response = await client.PostAsync(endpoint, multipartContent))
                {
                    if (Response.StatusCode == System.Net.HttpStatusCode.Created)
                    {
                        return RedirectToAction("index", "book");
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
            if (!ModelState.IsValid) return View();

            AuthorListDto authorListDto;
            GenreListDto genreListDto;
            BookPostDto bookDto;

            var authorUrl = "https://localhost:44305/admin/api/authors";
            var genreUrl = "https://localhost:44305/admin/api/genres";
            var bookUrl = "https://localhost:44305/admin/api/books/" + id;


            using (HttpClient client = new HttpClient())
            {

                var authorResponse = await client.GetAsync(authorUrl);
                var authorResponseStr = await authorResponse.Content.ReadAsStringAsync();

                var bookResponse = await client.GetAsync(bookUrl);
                var bookResponseStr = await bookResponse.Content.ReadAsStringAsync();


                var genreResponse = await client.GetAsync(genreUrl);
                var genreResponseStr = await genreResponse.Content.ReadAsStringAsync();

                if (genreResponse.StatusCode == System.Net.HttpStatusCode.OK && authorResponse.StatusCode == System.Net.HttpStatusCode.OK && bookResponse.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    authorListDto = JsonConvert.DeserializeObject<AuthorListDto>(authorResponseStr);
                    genreListDto = JsonConvert.DeserializeObject<GenreListDto>(genreResponseStr);
                    bookDto = JsonConvert.DeserializeObject<BookPostDto>(bookResponseStr);

                    BookPostVM bookVM = new BookPostVM
                    {
                        Authors = authorListDto,
                        Genres = genreListDto,
                        Books = bookDto

                    };
                    return View(bookVM);
                }
                else
                {
                    return Json(bookResponse.StatusCode);
                }
            }
        }


        [HttpPost]
        public async Task<IActionResult> Update(int id, BookPostVM bookVM)
        {
            if (!ModelState.IsValid) return View();
            using (HttpClient client = new HttpClient())
            {
                byte[] byteArr = null;

                if (bookVM.Books.ImageFile != null)
                {
                    using (var mStream = new MemoryStream())
                    {
                        bookVM.Books.ImageFile.CopyTo(mStream);
                        byteArr = mStream.ToArray();
                    }
                }
                else
                {
                    return BadRequest();
                }
                var byteArrContent = new ByteArrayContent(byteArr);
                byteArrContent.Headers.ContentType = MediaTypeHeaderValue.Parse(bookVM.Books.ImageFile.ContentType);
                var multipartContent = new MultipartFormDataContent();
                multipartContent.Add(byteArrContent, "ImageFile", bookVM.Books.ImageFile.FileName);
                multipartContent.Add(new StringContent(JsonConvert.SerializeObject(bookVM.Books.Name), Encoding.UTF8, "application/json"), "Name");
                multipartContent.Add(new StringContent(JsonConvert.SerializeObject(bookVM.Books.Language), Encoding.UTF8, "application/json"), "language");
                multipartContent.Add(new StringContent(JsonConvert.SerializeObject(bookVM.Books.CostPrice), Encoding.UTF8, "application/json"), "costprice");
                multipartContent.Add(new StringContent(JsonConvert.SerializeObject(bookVM.Books.SalePrice), Encoding.UTF8, "application/json"), "saleprice");
                multipartContent.Add(new StringContent(JsonConvert.SerializeObject(bookVM.Books.PageCount), Encoding.UTF8, "application/json"), "pagecount");
                multipartContent.Add(new StringContent(JsonConvert.SerializeObject(bookVM.Books.GenreId), Encoding.UTF8, "application/json"), "genreid");
                multipartContent.Add(new StringContent(JsonConvert.SerializeObject(bookVM.Books.AuthorId), Encoding.UTF8, "application/json"), "authorid");

                string endpoint = "https://localhost:44305/admin/api/books/" + id;
                using (var Response = await client.PutAsync(endpoint, multipartContent))
                {
                    if (Response.StatusCode == System.Net.HttpStatusCode.NoContent)
                    {
                        return RedirectToAction("index", "book");
                    }
                    else
                    {
                        return Json(Response.StatusCode);
                    }
                }
            }
        }

        public async Task<IActionResult> Delete(int id)
        {
            using (HttpClient client = new HttpClient())
            {
                var response = await client.DeleteAsync("https://localhost:44305/admin/api/books/" + id);

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
