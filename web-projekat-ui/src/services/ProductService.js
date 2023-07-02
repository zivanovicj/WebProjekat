import axios from 'axios';

export const GetProducts = async () => {
    //const api = process.env.API_URL;
    //console.log(api);
    return await axios.get('https://localhost:44365/api/Products/products')
}