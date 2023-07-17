import axios from 'axios';

export const GetProducts = async () => {
    return await axios.get(`${process.env.REACT_APP_API_URL}/Products`)
}

export const GetProduct = async (id) => {
    return await axios.get(`${process.env.REACT_APP_API_URL}/Products/details/${id}`)
}

export const GetSellerProducts = async () => {
    return await axios.get(`${process.env.REACT_APP_API_URL}/Products/${localStorage.getItem('email')}`, {
        headers: {
            'Authorization': 'Bearer ' + localStorage.getItem('token')
        }
    })
}

export const AddProduct = async (data) => {
    return await axios.post(`${process.env.REACT_APP_API_URL}/Products/addProduct`, data, {
        headers:{
            'Authorization': 'Bearer ' + localStorage.getItem('token')
        }
    })
}

export const UpdateProductPicture = async (data) => {
    return await axios.post(`${process.env.REACT_APP_API_URL}/Images/pimgUpdate/${data.productID}`, data.formData, {
        headers: {
            'Content-Type': 'multipart/form-data',
            "Authorization" : 'Bearer ' + data.token
        }
    })
}

export const UpdateProduct = async(data) => {
    return await axios.post(`${process.env.REACT_APP_API_URL}/Products/modify/${data.productID}`, data, {
        headers:{
            'Authorization': 'Bearer ' + localStorage.getItem('token')
        }
    })
}

export const DeleteProduct = async(productID) => {
    return await axios.delete(`${process.env.REACT_APP_API_URL}/Products/remove/${productID}`, {
        headers:{
            'Authorization': 'Bearer ' + localStorage.getItem('token')
        }
    })
}