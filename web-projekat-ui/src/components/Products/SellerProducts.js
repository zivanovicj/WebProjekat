import Product from "./Product";
import { useParams } from 'react-router-dom';
import { useEffect, useState } from 'react';
import { GetSellerProducts } from "../../services/ProductService";

function SellerProducts(){
    const {email} = useParams();
    const [products, setProducts] = useState([]);

    useEffect(() => {
        const get = async () => {
            await GetSellerProducts().then((response) => {
                setProducts(response.data)
            }).catch((error) => {})
        }

        get()
    },[email])

    return (
        <div style={{textAlign:"center"}}>
        <ul style={{display:"inline-block"}}>
          {products.map(product => (
            <Product
              key = {product.productID}
              product={product}
              modify = {true}
            >
            </Product>
          ))}
        </ul>
        </div>
    );
}

export default SellerProducts;