module.exports = () => {

    const input = document.getElementById('js-search-input');
    const list = document.getElementById('js-auto-suggest-container__list');

    if(input == null || list == null){
        return;
    }

    input.addEventListener('input', () => {
        const term = input.value;
        if (term.length >= 1) {
            fetch(`/umbraco/surface/SearchSurface/GetSuggestions?term=${term}`)
                .then(response => response.json())
                .then(suggestions => {
                    list.innerHTML = '';
                    suggestions.forEach(suggestion => {
                        const li = document.createElement('li');
                        li.textContent = suggestion;
                        li.addEventListener('click', () => {
                            const url = `/search/?q=${encodeURIComponent(suggestion)}`;
                            window.location.href = url;
                        });
                        list.appendChild(li);
                    });
                    list.style.display = 'block';
                })
                .catch(error => console.error(error));
        } else {
            list.style.display = 'none';
        }
    });

    document.addEventListener('click', () => {
        list.style.display = 'none';
    });
};