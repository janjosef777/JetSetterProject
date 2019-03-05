$(document).ready(function () {

    var url = '/api/NewsApi/';
    var country;
    var pageInt;

    $("<div class='row'>" +
        "<div class='col-xs-6 col-md-3'><button type='button' id='btn' value='cad' class='btn btn-info btn-lg btn-block'>Get Canadian Articles</button></div>" +
        "<div class='col-xs-6 col-md-3'><button type='button' id='btn' value='usa' class='btn btn-info btn-lg btn-block'>Get USA Articles</button></div>" +
        "</div>"
    ).appendTo(".newsOptions");


    $('.btn-info').on('click', function (e) {
        var checkCountry = $(this).attr("value");
        $('.btn-info').each(function () {
            $('.btn-info').removeClass('active');
        })
        if (checkCountry == "cad") {
            $(this).addClass('active');
            country = "ca";
            pageInt = 1;
            newsAPI(country, pageInt);
        }
        else if (checkCountry == "usa") {
            $(this).addClass('active');
            country = "us";
            pageInt = 1;
            newsAPI(country, pageInt);
        }

    });

    class ArticleItem {
        constructor(ImageURL, Title, Author, Source, Description, Day, Month, URL) {
            this.Date = new DateItem(Day, Month);
            this.ImageURL = ImageURL != null ? ImageURL : "https://cdn1.iconfinder.com/data/icons/office-1/128/4-512.png";
            this.Title = Title;
            this.Author = Author != null ? Author : "No Author";
            this.Source = Source;
            this.Description = Description != null ? Description : "Read more...";
            this.URL = URL;
        }
    }
        class DateItem {
        constructor(Day, Month) {
            this.Day = Day != 0 ? Day : 'N/A';
            this.Month = Month;
        }
    }
    function newsAPI(country, pageInt) {
        $(".post featured").remove();
        $(".post").remove();
        $(".btn-default").remove();
        $.ajax({
            type: 'GET',
            url: url + country + '/' + pageInt,
            dataType: 'json',
            success: function (data) {
                var totalResults = data.totalResults;
                var month = ["Jan", "Feb", "Mar", "Apr", "May", "Jun",
                    "Jul", "Aug", "Sep", "Oct", "Nov", "Dec"];
                for (var i = 0, j = data.articles.length; i < j; i++) {

                    var article = new ArticleItem(data.articles[i].urlToImage, data.articles[i].title, data.articles[i].author, data.articles[i].source, data.articles[i].description,
                        new Date(data.articles[i].publishedAt).getDay(), month[new Date(data.articles[i].publishedAt).getMonth()], data.articles[i].url);

                    if (i == 0) {
                        console.log("I: " + i + "J: " + j);
                        buildFeatured(article.ImageURL, article.Title, article.Description, article.Date, article.Source, article.URL, article.Author);
                    }
                    else if (i > 0) {
                        buildNormal(article.ImageURL, article.Title, article.Description, article.Date, article.Source, article.URL, article.Author, appendEl);
                    }
                    if (i == 0 || i % 2 == 0) {
                        var appendEl = $("<div class='row cf'></div>").appendTo(".blog-posts");
                    }
                    if (totalResults > 0) {
                        totalResults = totalResults - 10 > 0 ? totalResults - 10 : 0;
                        $("<button type='button' id='pageId' value=" + (i + 1) + " class='btn btn-default btn-lg'>" + (i + 1) + "</button></div>"
                            + "</div >").appendTo(".containerCard");
                        if (i == pageInt-1) {
                            $('.btn[value=' + pageInt +']').addClass('active');
                        }
                    }
                }
            }
        });
        function buildFeatured(imgUrl, title, body, date, source, urlToArticle, author) {
            $("<div class='post featured'>" +
                "<a href='" + urlToArticle + "'>" +
                "<div class='image' style='background-image: url(" + imgUrl + ")'>" +
                "<div class='time'>" +
                "<div class='date'>" + date.Day + "</div>" +
                "<div class='month'>" + date.Month + "</div>" +
                "</div>" +
                "</div>" +
                "<div class='content'>" +
                "<h1>" + title + "</h1>" +
                "<p>" + body + "</p>" +
                "<div class='meta'>" +
                "<div class='icon-comment'>Author: " + author + "</div>" +
                "<ul class='tags'>" +
                "<li>" + (source.id != null ? source.id : "No ID") + "</li>" +
                "<li>" + (source.name != null ? source.name : "No Name") + " </li>" +
                "</ul>" +
                "</div>" +
                "</div>" +
                "</a>" +
                "</div>"
            ).appendTo(".blog-posts");
        }
        function buildNormal(imgUrl, title, body, date, source, urlToArticle, author, appendItem) {
            $("<div class='post'>" +
                "<a href='" + urlToArticle + "'>" +
                "<div class='image' style='background-image: url(" + imgUrl + ")'>" +
                "<div class='time'>" +
                "<div class='date'>" + date.Day + "</div>" +
                "<div class='month'>" + date.Month + "</div>" +
                "</div>" +
                "</div>" +
                "<div class='content'>" +
                "<h1>" + title + "</h1>" +
                "<p>" + body + "</p>" +
                "<div class='meta'>" +
                "<div class='icon-comment'>" + author + "</div>" +
                "<ul class='tags'>" +
                "<li>" + (source.id != null ? source.id : "No Source") + "</li>" +
                "<li>" + (source.name != null ? source.name : "No Source") + " </li>" +
                "</ul>" +
                "</div>" +
                "</div>" +
                "</a>" +
                "</div>"
            ).appendTo(appendItem);
        }

    }
    $('.containerCard').on('click', 'button', function(){
        pageInt = $(this).attr("value");
        newsAPI(country, pageInt);
    });
});