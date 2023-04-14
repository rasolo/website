module.exports = () => {
    const loadMore = document.querySelector('#js-load-more-posts');
    const loadAll = document.querySelector('#js-load-all-posts');
    const postsLists = document.querySelector('#js-posts-container__posts');
    let pageIndex = 1;
  
    const appendPosts = (json) => {
      json.forEach((element) => {
        const article = document.createElement('article');
        article.className = 'posts__post';
  
        const blogPostAnchor = document.createElement('a');
        blogPostAnchor.setAttribute('href', element.BlogPostUrl);
  
        const img = document.createElement('img');
        img.className ="post__image";
        img.setAttribute('src', element.TeaserUrl);
        blogPostAnchor.append(img);
  
        article.append(blogPostAnchor);
  
        const blogAnchor = document.createElement('a');
        blogAnchor.setAttribute('href', element.ParentUrl);
        blogAnchor.innerText = element.ParentName;
        blogAnchor.className = 'post__category';
  
        article.append(blogAnchor);
  
        const blogPostDateAnchor = document.createElement('a');
        blogPostDateAnchor.setAttribute('href', element.BlogPostUrl);
  
        const blogPostDateParagraph = document.createElement('p');
        blogPostDateParagraph.innerText = element.CreateDate;
        blogPostDateAnchor.append(blogPostDateParagraph);
  
        const blogPostDateHeading = document.createElement('h4');
        blogPostDateHeading.innerText = element.TeaserHeading;
        blogPostDateAnchor.append(blogPostDateHeading);
  
        article.append(blogPostDateAnchor);
        postsLists.append(article);
      });
    };
  
    const fetchPosts = (all = false) => {
      const url = window.location + 'Umbraco/Api/blogpostapi/getblogposts?' + new URLSearchParams({
        p: pageIndex,
        all: all,
      });
  
      fetch(url)
        .then((response) => response.json())
        .then((json) => {
  
          if (!all) {
            if (json.length < 5) {
                removeButtons();
            } else {
              json.pop();
            }
          }
          appendPosts(json);
          pageIndex++;
        })
        .catch((err) => console.warn('Error in getting blog posts through ajax.', err));
    };

    function removeButtons() {
        loadMore.style.display = 'none';
        loadAll.style.display = 'none';
    }
  
    loadAll.addEventListener('click', () => {
      fetchPosts(true);
      removeButtons();
    });
  
    loadMore.addEventListener('click', () => {
      fetchPosts();
    });
  };