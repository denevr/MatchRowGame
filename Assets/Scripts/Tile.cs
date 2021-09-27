using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Tile : MonoBehaviour
{
	private static Tile previousSelected = null;

	private SpriteRenderer render;
	List<GameObject> matchTiles = new List<GameObject>();
	private Vector2[] unitVectors = new Vector2[] { Vector2.up, Vector2.down, Vector2.left, Vector2.right };
	private bool matchFound = false;
	private bool isSelected = false;
	private bool isSelectable;

	void Awake() 
	{
		render = GetComponent<SpriteRenderer>();
		//Physics2D.queriesStartInColliders = false; //if raycast is hitting itself, fix it
	}

    void Start()
    {
        isSelectable = true;
    }

    void Update()
    {
        if (render.sprite == GridManager.instance.endSprite)
        {
            isSelectable = false;
        }
    }

    private void Select() 
	{
		isSelected = true;
		render.color = Color.grey;
		previousSelected = gameObject.GetComponent<Tile>();
	}

	private void Deselect() 
	{
		isSelected = false;
		render.color = Color.white;
		previousSelected = null;
	}

	void OnMouseDown()
	{
		if (isSelectable)
		{ 
			if (render.sprite == null)
			{
				return;
			}

			if (isSelected)
			{
				Deselect();
			}
			else
			{
				if (previousSelected == null)
				{
					Select();
				}
				else
				{
					if (GetAllAdjacentTiles().Contains(previousSelected.gameObject))
					{
						SwapTiles(previousSelected.render);
						previousSelected.ClearAllMatches();
						previousSelected.Deselect();
						ClearAllMatches();
					}
					else
					{
						previousSelected.GetComponent<Tile>().Deselect();
						Select();
					}
				}

			}
		}
	}

	public void SwapTiles(SpriteRenderer render2)
	{
		if (render.sprite == render2.sprite)
			return;

		Sprite sprite = render2.sprite;
		render2.sprite = render.sprite;
		render.sprite = sprite;

        GUIManager.instance.MoveCounter--;
    }

	private GameObject GetAdjacent(Vector2 direction)
	{
		RaycastHit2D hit = Physics2D.Raycast(transform.position, direction);
		if (hit.collider != null)
			return hit.collider.gameObject;
		else
			return null;
	}

	private List<GameObject> GetAllAdjacentTiles()
	{
		List<GameObject> adjacentTiles = new List<GameObject>();
		for (int i = 0; i < unitVectors.Length; i++)
		{
			adjacentTiles.Add(GetAdjacent(unitVectors[i]));
		}
		return adjacentTiles;
	}

	private List<GameObject> FindMatch(Vector2 direction)
	{
		List<GameObject> matchingTiles = new List<GameObject>();
		RaycastHit2D hit = Physics2D.Raycast(transform.position, direction);

		while (hit.collider != null && hit.collider.GetComponent<SpriteRenderer>().sprite == render.sprite)
		{
			matchingTiles.Add(hit.collider.gameObject);
			hit = Physics2D.Raycast(hit.collider.transform.position, direction);
		}
		return matchingTiles;
	}

	private void ClearMatch(Vector2[] paths)
	{
		int matchingScore = 0;
		List<GameObject> matchingTiles = new List<GameObject>();
		for (int i = 0; i < paths.Length; i++)
		{
			matchingTiles.AddRange(FindMatch(paths[i]));
		}
		if (matchingTiles.Count >= GridManager.instance.matchingTileCount)//4) 
		{
			if (matchingTiles[matchingTiles.Count - 1].GetComponent<SpriteRenderer>().sprite == GridManager.instance.shapes[0]) //blue
				matchingScore = 200;
			if (matchingTiles[matchingTiles.Count - 1].GetComponent<SpriteRenderer>().sprite == GridManager.instance.shapes[1]) //green
				matchingScore = 150;
			if (matchingTiles[matchingTiles.Count - 1].GetComponent<SpriteRenderer>().sprite == GridManager.instance.shapes[2]) //yellow
				matchingScore = 250;
			if (matchingTiles[matchingTiles.Count - 1].GetComponent<SpriteRenderer>().sprite == GridManager.instance.shapes[3]) //red
				matchingScore = 100;

			for (int i = 0; i < matchingTiles.Count; i++)
			{
				matchingTiles[i].GetComponent<SpriteRenderer>().sprite = GridManager.instance.endSprite;
			}
			GUIManager.instance.Score += (GridManager.instance.matchingTileCount + 1) * matchingScore;
			matchFound = true;
		}
	}

	public void ClearAllMatches()
	{
		if (render.sprite == null)
			return;

		ClearMatch(new Vector2[2] { Vector2.left, Vector2.right });

		if (matchFound)
		{
			render.sprite = GridManager.instance.endSprite;
			isSelectable = false;
			matchFound = false;
		}
	}
}

