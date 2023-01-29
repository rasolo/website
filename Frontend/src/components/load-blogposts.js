module.exports = function () {
    const loadMore = document.querySelectorAll(".posts-container__load-more-container__text")[0];
    const postsLists = document.querySelectorAll(".posts-container__posts")[0];
    let pageIndex = 1;
 
    loadMore.onclick = function() {
  

        fetch(window.location + 'Umbraco/Api/blogpostapi/getblogposts?' + new URLSearchParams({
            p: pageIndex
        })).then(async function (response) {
            // The API call was successful!
            var json = await response.json();
            if(json.length === 0){
                loadMore.style.display = 'none';
            }
            json.forEach((element) => {
                var article = document.createElement('article');
                article.className = 'posts__post';
        
                var blogPostAnchor = document.createElement('a');
                blogPostAnchor.setAttribute('href', element.BlogPostUrl);
    
                var img = document.createElement('img');
                img.setAttribute('src', element.TeaserUrl);
                blogPostAnchor.append(img);

                article.append(blogPostAnchor);

                var blogAnchor = document.createElement('a');
                blogAnchor.setAttribute('href', element.ParentUrl);
                blogAnchor.innerText = element.ParentName;
                blogAnchor.className = 'post__category';

                article.append(blogAnchor);

                var blogPostDateAnchor = document.createElement('a');
                blogPostDateAnchor.setAttribute('href', element.BlogPostUrl);
                var blogPostDateParagraph = document.createElement('p');
                blogPostDateParagraph.innerText = element.CreateDate;

                blogPostDateAnchor.append(blogPostDateParagraph);
                var blogPostDateHeading = document.createElement('h4');
                blogPostDateHeading.innerText = element.TeaserHeading;
                blogPostDateAnchor.append(blogPostDateHeading);

                article.append(blogPostDateAnchor);
                postsLists.append(article)
            });
     


        }).then(function () {
            pageIndex = pageIndex + 1;
        }).catch(function (err) {
            // There was an error
            console.warn('Error in getting blog posts through ajax.', err);
        });
};
}