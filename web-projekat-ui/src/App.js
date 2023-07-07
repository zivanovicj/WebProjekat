import { useEffect, useState } from 'react';
import './App.css';
import Navigation from './components/Navigation';
import { GetProducts } from './services/ProductService';
import ProductList from './components/Products/ProductList.js';
import {Routes, Route} from 'react-router-dom';
import ProductDetails from './components/Products/ProductDetails';
import LogInForm from './components/Users/LogInForm';
import UserProfile from './components/Users/UserProfile';
import UpdateProfile from './components/Users/UpdateProfile';
import AdminUserList from './components/Users/AdminUserList';

function App() {
  const [products, setProducts] = useState([]);
  
  useEffect(()=>{
    const get = async() => {
      await GetProducts().then((response) => {
        setProducts(response.data);
      }).catch((error) => {
      });
    }
    get();
  }, [])

  return (
    <>
    <Navigation></Navigation>
    <Routes>
      <Route path="/" element={<ProductList products={products}/>}></Route>
      <Route path="/details/:id" element={<ProductDetails/>}></Route>
      <Route path="/login" element={<LogInForm/>}></Route>
      <Route path='/profile/:email' element={<UserProfile/>}></Route>
      <Route path='/updateProfile/:email' element={<UpdateProfile/>}></Route>
      <Route path='/customers' element={<AdminUserList/>}></Route>
      <Route path='/sellers' element={<AdminUserList/>}></Route>
    </Routes>
    </>
  );
}

export default App;
