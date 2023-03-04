module.exports = function () {
    const loadMore = document.querySelectorAll(".posts-container__load-more-container__item")[0];
    const loadAll = document.querySelectorAll(".posts-container__load-more-container__item--all")[0];
    const postsLists = document.querySelectorAll(".posts-container__posts")[0];
    let pageIndex = 1;

    loadAll.onclick = function() {
        fetch(window.location + 'Umbraco/Api/blogpostapi/getblogposts?' + new URLSearchParams({
            p: pageIndex,
            all: true
        })).then(async function(response) {
            var json = await response.json();
            loadMore.style.display = 'none';
            loadAll.style.display = 'none';

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
        })
    }
 
    loadMore.onclick = function() {
        fetch(window.location + 'Umbraco/Api/blogpostapi/getblogposts?' + new URLSearchParams({
            p: pageIndex
        })).then(async function (response) {
            // The API call was successful!
            var json = await response.json();
            if(json.length < 5){ 
                loadMore.style.display = 'none';
            } else {
                json.pop();
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