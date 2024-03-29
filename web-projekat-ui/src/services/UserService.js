import axios from 'axios';

export const LogIn = async (loginform) =>{
    return await axios.post(`${process.env.REACT_APP_API_URL}/Users/login`, loginform);
}


export const GoogleLogIn = async (info) => {
    return await axios.get('https://www.googleapis.com/oauth2/v1/userinfo?access_token=' + info.access_token,
                            {
                                headers: {
                                    Authorization: `Bearer ${info.access_token}`,
                                    Accept: 'application/json'
                                }
                            })
}

export const LogInGoogle = async (info) => {
    let header = {
        Email:info.email,
        FirstName: 'as',
        LastName: 'as'
    }
    if(Object.keys(info).includes('given_name'))
        header.FirstName = info.given_name
    if(Object.keys(info).includes('family_name'))
        header.LastName = info.family_name

    return await axios.post(`${process.env.REACT_APP_API_URL}/Users/googleLogin`, header)
}

export const GetUserDetails = async (info) => {
    return await axios.get(`${process.env.REACT_APP_API_URL}/Users/${info.email}`,
        {
            headers: {
                "Authorization" : 'Bearer ' + info.token
            }
        }
    )
}

export const UpdateUserInfo = async (info) => {
    return await axios.post(`${process.env.REACT_APP_API_URL}/Users/update`, info.user,
    {
        headers: {
            "Authorization" : 'Bearer ' + info.token
        }
    }
    )
}

export const UpdateUserPicture = async (data) => {
    return await axios.post(`${process.env.REACT_APP_API_URL}/Users/usrimgUpdate`, data.formData, {
        headers: {
            'Content-Type': 'multipart/form-data',
            "Authorization" : 'Bearer ' + data.token
        }
    })
}

export const UserRegistration = async (newUser) => {
    return await axios.post(`${process.env.REACT_APP_API_URL}/Users/register`, newUser);
}

export const ChangePassword = async (data) => {
    return await axios.post(`${process.env.REACT_APP_API_URL}/Users/passwordChange`, data, {
        headers:{
            'Authorization': 'Bearer ' + localStorage.getItem('token')
        }
    })
}