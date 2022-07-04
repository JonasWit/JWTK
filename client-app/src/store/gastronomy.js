import {getIngredients, removeIngredient, updateIngredient} from "@/services/gastronomyServiceAPI";

export default {
    namespaced: true,
    state: {
        ingredients: []
    },
    getters: {
        listOfingredients(state){
            return state.ingredients;
        },
    },
    mutations: {
        updateIngredientsList(state, { ingredientsList }){
            state.ingredients = ingredientsList
        },
        deleteIngredient(state, id){
            state.ingredients.splice(id) 
        },
        updateOneIngredientOnList(state, { updatedIngredient }){
            state.ingredients.splice(updatedIngredient.id)
            state.ingredients.push(updatedIngredient)
        }
    },
    actions: {
        async getAllIngredients({commit}) {
            try {
                const res = await getIngredients()
                const results = res.data
                results.sort((a, b) => {
                    const idA = a.id;
                    const idB = b.id;
                    return idB - idA;
                })
                commit('updateIngredientsList', { ingredientsList: results })
             } catch (error) {
                console.log(error)}
        }, 
        async deleteOneIngredient({commit}, id) {
            try {
                console.log("id",id)
                await removeIngredient(id)
                commit('deleteIngredient')
                } catch (error) {
                console.log(error)}
        },    
        async updateOneIngredient({commit}, item) {
            try {
                await updateIngredient(item)
                commit('updateOneIngredientOnList', { updatedIngredient: item })   
                } catch (error) {
                console.log(error)}
        },  
        
    }
    
}