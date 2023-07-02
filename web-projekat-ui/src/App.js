import { useEffect, useState } from 'react';
import './App.css';
import Navigation from './components/Navigation';
import { GetProducts } from './services/ProductService';
import ProductList from './components/Products/ProductList';

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
    <div className="App">
      <Navigation></Navigation>
      <ProductList products={products}></ProductList>
    </div>
  );
}

export default App;
