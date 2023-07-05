import { useEffect, useState } from 'react';
import './App.css';
import Navigation from './components/Navigation';
import { GetProducts } from './services/ProductService';
import ProductList from './components/Products/ProductList.js';
import {Routes, Route} from 'react-router-dom';
import ProductDetails from './components/Products/ProductDetails';
import LogInForm from './components/Users/LogInForm';
import UserProfile from './components/Users/UserProfile';

function App() {
  const [products, setProducts] = useState([]);
  
  useEffect(()=>{
    const get = async() => {
      const response = await GetProducts();
      setProducts(response.data);
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
    </Routes>
    </>
  );
}

export default App;
