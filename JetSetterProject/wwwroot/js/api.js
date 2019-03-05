$(document).ready(function () {
    class ArticleItem {
        constructor(ImageURL, Title, Author, Source, Description, Day, Month, URL) {
            this.Date = new DateItem(Day, Month);
            this.ImageURL = ImageURL != null ? ImageURL : "https://cdn1.iconfinder.com/data/icons/office-1/128/4-512.png";
            this.Title = Title;
            this.Author = Author != null ? Author : "No Author";
            this.Source = Source;
            this.Description = Description;
            this.URL = URL;
        }
    }
        class DateItem {
        constructor(Day, Month) {
            this.Day = Day;
            this.Month = Month;
        }
    }
    $.ajax({
        type: 'GET',
        url: '/api/NewsApi',
        dataType: 'json',
        success: function (data) {
            var month = ["Jan", "Feb", "Mar", "Apr", "May", "Jun",
                "Jul", "Aug", "Sep", "Oct", "Nov", "Dec"];
            for (var i = 0, j = data.articles.length; i < j; i++) {

                var article = new ArticleItem(data.articles[i].urlToImage, data.articles[i].title, data.articles[i].author, data.articles[i].source, data.articles[i].description,
                    new Date(data.articles[i].publishedAt).getDay(), month[new Date(data.articles[i].publishedAt).getMonth()], data.articles[i].url);

                if (i == 0) {
                    buildFeatured(article.ImageURL, article.Title, article.Description, article.Date, article.Source, article.URL, article.Author);
                }
                else {
                    buildNormal(article.ImageURL, article.Title, article.Description, article.Date, article.Source, article.URL, article.Author, appendEl);
                }
                if (i == 0 || i % 2 == 0) {
                    var appendEl = $("<div class='row cf'></div>").appendTo(".blog-posts");
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
            "<li>" + (source.id != null ? source.id : "No Source")  + "</li>" +
            "<li>" + (source.name != null ? source.name : "No Source") + " </li>" +
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
            "<li>" + (source.name != null ? source.name : "No Source")  + " </li>" +
            "</ul>" +
            "</div>" +
            "</div>" +
            "</a>" +
            "</div>"
        ).appendTo(appendItem);
    }
});