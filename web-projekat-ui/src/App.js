import { useEffect, useState } from 'react';
import './App.css';
import Navigation from './components/Navigation';
import { GetProducts } from './services/ProductService';
import ProductList from './components/Products/ProductList';
import {Routes, Route} from 'react-router-dom';
import ProductDetails from './components/Products/ProductDetails';

function App() {
  const [products, setProducts] = useState([]);
  
  useEffect(()=>{
    const get = async() => {
      const response = await GetProducts();
      console.log(response.data);
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
    </Routes>
    </>
  );
}

export default App;
