import axios from 'axios';

export const GetProducts = async () => {
    //const api = process.env.API_URL;
    //console.log(api);
    return await axios.get('https://localhost:44365/api/Products')
}

export const GetProduct = async (id) => {
    return await axios.get('https://localhost:44365/api/Products/details/' + id)
}

export const GetSellerProducts = async () => {
    return await axios.get('https://localhost:44365/api/Products/' + localStorage.getItem('email'), {
        headers: {
            'Authorization': 'Bearer ' + localStorage.getItem('token')
        }
    })
}

export const AddProduct = async (data) => {
    return await axios.post('https://localhost:44365/api/Products/addProduct', data, {
        headers:{
            'Authorization': 'Bearer ' + localStorage.getItem('token')
        }
    })
}