async function getJsonFromUrl(url) {
    try {
        const response = await fetch(url);
        if (!response.ok) {
            throw new Error(`HTTP error! status: ${response.status}`);
        }
        const data = await response.json();
        return data;
    } catch (error) {
        console.error('There was a problem with the fetch operation: ', error);
    }
}

/* Usage
getJsonFromUrl('https://api.example.com/data')
    .then(data => console.log(data))
    .catch(error => console.error('Error:', error)); */
